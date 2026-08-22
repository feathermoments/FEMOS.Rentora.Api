using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for loading authorization-related data.
    /// Used during login and token refresh to build JWT claims with:
    /// - Property -> Role mappings for the user
    /// - Role -> Permission mappings (de-duplicated)
    /// </summary>
    internal class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IDBHelper _dbHelper;

        public AuthorizationRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        /// <summary>
        /// Loads all properties and roles for a user from Gen_PropertyMembers.
        /// Returns: PropertyPublicId, RoleId, RoleCode for each property the user is a member of.
        /// </summary>
        public async Task<List<PropertyRoleInfo>> GetUserPropertyRolesAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetUserPropertyRoles);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            var propertyRoles = new List<PropertyRoleInfo>();

            foreach (DataRow row in dt.Rows)
            {
                propertyRoles.Add(new PropertyRoleInfo
                {
                    PropertyPublicId = row.Table.Columns.Contains("PropertyPublicId") 
                        ? (Guid)row["PropertyPublicId"] 
                        : Guid.Empty,
                    RoleId = row.Table.Columns.Contains("RoleId") 
                        ? Convert.ToInt64(row["RoleId"]) 
                        : 0,
                    RoleCode = row.Table.Columns.Contains("RoleCode") 
                        ? row["RoleCode"]?.ToString() ?? string.Empty 
                        : string.Empty
                });
            }

            return propertyRoles;
        }

        /// <summary>
        /// Loads permissions for a specific role from Mst_RolePermissions.
        /// Returns: List of permission codes for the role (e.g., "PROPERTY.VIEW", "PROPERTY.EDIT")
        /// </summary>
        public async Task<List<string>> GetRolePermissionsAsync(long roleId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetRolePermissions);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleId", roleId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            var permissions = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                var permissionCode = row.Table.Columns.Contains("PermissionCode")
                    ? row["PermissionCode"]?.ToString() ?? string.Empty
                    : string.Empty;

                if (!string.IsNullOrEmpty(permissionCode))
                {
                    permissions.Add(permissionCode);
                }
            }

            return permissions;
        }

        /// <summary>
        /// Loads permissions for multiple roles efficiently.
        /// Returns: RoleId -> List of permission codes mapping.
        /// Prevents duplicate permission queries for users with multiple properties of the same role.
        /// </summary>
        public async Task<Dictionary<long, List<string>>> GetRolePermissionsAsync(List<long> roleIds)
        {
            if (roleIds == null || roleIds.Count == 0)
                return new Dictionary<long, List<string>>();

            // Get distinct role IDs to avoid duplicate queries
            var distinctRoleIds = roleIds.Distinct().ToList();

            var cmd = new SqlCommand(DBConstants.sp_GetMultipleRolePermissions);
            cmd.CommandType = CommandType.StoredProcedure;

            // Create a DataTable for table-valued parameter
            // This allows passing a list of RoleIds to the stored procedure
            var roleIdTable = new DataTable();
            roleIdTable.Columns.Add("RoleId", typeof(long));

            foreach (var roleId in distinctRoleIds)
            {
                roleIdTable.Rows.Add(roleId);
            }

            var roleIdParam = new SqlParameter("@RoleIds", SqlDbType.Structured)
            {
                TypeName = "dbo.RoleIdTableType",
                Value = roleIdTable
            };
            cmd.Parameters.Add(roleIdParam);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            var result = new Dictionary<long, List<string>>();

            // Initialize all role IDs with empty lists
            foreach (var roleId in distinctRoleIds)
            {
                result[roleId] = new List<string>();
            }

            // Populate permissions from result set
            foreach (DataRow row in dt.Rows)
            {
                var roleId = row.Table.Columns.Contains("RoleId")
                    ? Convert.ToInt64(row["RoleId"])
                    : 0;

                var permissionCode = row.Table.Columns.Contains("PermissionCode")
                    ? row["PermissionCode"]?.ToString() ?? string.Empty
                    : string.Empty;

                if (roleId > 0 && !string.IsNullOrEmpty(permissionCode))
                {
                    if (!result.ContainsKey(roleId))
                    {
                        result[roleId] = new List<string>();
                    }

                    result[roleId].Add(permissionCode);
                }
            }

            return result;
        }
    }
}
