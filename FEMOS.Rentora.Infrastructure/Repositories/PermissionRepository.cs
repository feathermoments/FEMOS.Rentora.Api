using FEMOS.Rentora.Domain.Constants;
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
    internal class PermissionRepository : IPermissionRepository
    {
        private readonly IDBHelper _dbHelper;

        public PermissionRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<PermissionInfo>> GetUserPermissionsByRoleAsync(Guid userPublicId, long roleId)
        {
            var cmd = new SqlCommand(DBConstants.USP_User_GetPermissions);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@RoleId", roleId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<PermissionInfo>(dt);
        }

        /// <summary>
        /// Get all properties accessible to the user with their internal role for each property.
        /// </summary>
        public async Task<List<AccessiblePropertyInfo>> GetAccessiblePropertiesAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_User_GetAccessibleProperties);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<AccessiblePropertyInfo>(dt);
        }

        /// <summary>
        /// Get permissions for a user on a specific property (property-context).
        /// Returns property context and permissions as separate result sets.
        /// </summary>
        public async Task<(PropertyContextInfo Context, List<string> PermissionCodes)> GetPropertyPermissionsAsync(
            Guid userPublicId, Guid PropertyUniqueId)
        {
            var cmd = new SqlCommand(DBConstants.USP_User_GetPropertyPermissions);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyUniqueId", PropertyUniqueId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

            PropertyContextInfo context = null;
            List<string> permissionCodes = new List<string>();

            // First result set: Property context
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                context = new PropertyContextInfo
                {
                    PropertyUniqueId = (Guid)row["PropertyUniqueId"],
                    InternalRoleId = (long)row["InternalRoleId"],
                    InternalRoleCode = row["InternalRoleCode"]?.ToString() ?? "",
                    InternalRoleName = row["InternalRoleName"]?.ToString() ?? ""
                };
            }
            else
            {
                // No access to this property
                return (null, new List<string>());
            }

            // Second result set: Permissions
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    var permCode = row["PermissionCode"]?.ToString();
                    if (!string.IsNullOrEmpty(permCode))
                    {
                        permissionCodes.Add(permCode);
                    }
                }
            }

            permissionCodes = permissionCodes.OrderBy(p => p).ToList();
            return (context, permissionCodes);
        }

        // Kept for backward compatibility with previous implementation
        public async Task<(PropertyRoleInfo PropertyRole, List<PermissionInfo> Permissions)> GetUserPermissionsByPropertyAsync(Guid userPublicId, long propertyId)
        {
            var cmd = new SqlCommand(DBConstants.USP_User_GetPermissionsByProperty);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt.Rows.Count == 0)
            {
                return (null, new List<PermissionInfo>());
            }

            var firstRow = dt.Rows[0];
            var propertyRole = new PropertyRoleInfo
            {
                PropertyId = Convert.ToInt64(firstRow["PropertyId"]),
                RoleId = Convert.ToInt64(firstRow["RoleId"]),
                RoleCode = "",
                RoleName = "" 
            };

            var permissions = _dbHelper.ConvertDataTable<PermissionInfo>(dt);
            return (propertyRole, permissions);
        }

        // Kept for backward compatibility with previous implementation
        public async Task<PropertyRoleInfo> GetPropertyRoleInfoAsync(Guid userPublicId, long propertyId)
        {
            var cmd = new SqlCommand(DBConstants.USP_User_GetPropertyRoleInfo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            var roles = _dbHelper.ConvertDataTable<PropertyRoleInfo>(dt);
            return roles[0];
        }
    }
}


