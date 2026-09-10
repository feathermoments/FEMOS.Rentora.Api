using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IDBHelper _dbHelper;

        public PropertyRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<MyPropertyInfo>> GetMyPropertiesAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetMyProperties);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

            var properties = _dbHelper.ConvertDataTable<MyPropertyInfo>(ds.Tables[0]);
            var tenantAssignmentSummary = _dbHelper.ConvertDataTable<TenantAssignmentSummaryInfo>(ds.Tables[1]);
            var propertyQuickSummary = _dbHelper.ConvertDataTable<PropertyQuickSummaryInfo>(ds.Tables[2]);
            if (tenantAssignmentSummary.Any())
            {
                List<MyPropertyInfo> objProperties = new List<MyPropertyInfo>();
                foreach (TenantAssignmentSummaryInfo tenantAssignmentSummaryInfo in tenantAssignmentSummary)
                {
                    MyPropertyInfo objProperty = properties.FirstOrDefault(p => p.PropertyPublicId == tenantAssignmentSummaryInfo.PropertyPublicId);
                    if (objProperty != null)
                    {
                        // Create a new instance to avoid reference sharing issues
                        MyPropertyInfo objPropertyCopy = new MyPropertyInfo
                        {
                            PropertyPublicId = objProperty.PropertyPublicId,
                            PropertyName = objProperty.PropertyName,
                            PropertyTypeId = objProperty.PropertyTypeId,
                            PropertyType = objProperty.PropertyType,
                            City = objProperty.City,
                            State = objProperty.State,
                            AddressLine1 = objProperty.AddressLine1,
                            RoleId = objProperty.RoleId,
                            UserRole = objProperty.UserRole,
                            CoverImageUrl = objProperty.CoverImageUrl,
                            objTenantAssignmentSummaryInfo = tenantAssignmentSummaryInfo,
                            objPropertyQuickSummaryInfo = propertyQuickSummary.FirstOrDefault(p => p.PropertyPublicId == objProperty.PropertyPublicId)
                        };
                        objProperties.Add(objPropertyCopy);
                    }
                }
                return objProperties;
            }
            else
            {
                foreach (var property in properties)
                {
                    property.objTenantAssignmentSummaryInfo = tenantAssignmentSummary.FirstOrDefault(t => t.PropertyPublicId == property.PropertyPublicId);
                    property.objPropertyQuickSummaryInfo = propertyQuickSummary.FirstOrDefault(p => p.PropertyPublicId == property.PropertyPublicId);
                }
                return properties;
            }
        }

        public async Task<MyPropertiesSummaryResponseInfo> GetMyPropertiesSummaryAsync(Guid userPublicId)
        {
            MyPropertiesSummaryResponseInfo objResponseInfo = new MyPropertiesSummaryResponseInfo();

            var cmd = new SqlCommand(DBConstants.sp_GetMyPropertiesSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

            if (ds.Tables.Count == 0)
            {
                return objResponseInfo;
            }

            // Table 0: Role information (roleId, roleName)
            var roles = _dbHelper.ConvertDataTable<RoleSummaryInfo>(ds.Tables[0]);

            if (roles == null || roles.Count == 0)
            {
                return objResponseInfo;
            }

            var hasOwnerRole = roles.Any(r => r.RoleCode?.ToUpper() == "OWNER" || r.RoleCode?.ToUpper() == "CO_OWNER");
            var hasTenantRole = roles.Any(r => r.RoleCode?.ToUpper() == "TENANT" || r.RoleCode?.ToUpper() == "FAMILY_MEMBER");

            int tableIndex = 1;

            // Determine which summary tables are present and bind them accordingly
            if (hasOwnerRole && hasTenantRole)
            {
                // Table 1: OwnerSummaryInfo
                if (tableIndex < ds.Tables.Count)
                {
                    var ownerSummaries = _dbHelper.ConvertDataTable<OwnerSummaryInfo>(ds.Tables[tableIndex]);
                    if (ownerSummaries != null && ownerSummaries.Count > 0)
                    {
                        objResponseInfo.objOwnerSummary = ownerSummaries[0];
                    }
                    tableIndex++;
                }

                // Table 2: TenantSummaryInfo
                if (tableIndex < ds.Tables.Count)
                {
                    var tenantSummaries = _dbHelper.ConvertDataTable<TenantSummaryInfo>(ds.Tables[tableIndex]);
                    if (tenantSummaries != null && tenantSummaries.Count > 0)
                    {
                        objResponseInfo.objTenantSummary = tenantSummaries[0];
                    }
                    tableIndex++;
                }

                // Table 3: MonthlyTrendInfo
                if (tableIndex < ds.Tables.Count)
                {
                    var monthlyTrends = _dbHelper.ConvertDataTable<MonthlyTrendInfo>(ds.Tables[tableIndex]);
                    if (monthlyTrends != null && monthlyTrends.Count > 0)
                    {
                        objResponseInfo.objMonthlyTrends = monthlyTrends;
                    }
                }
            }
            else if (hasOwnerRole)
            {
                // Table 1: OwnerSummaryInfo
                if (tableIndex < ds.Tables.Count)
                {
                    var ownerSummaries = _dbHelper.ConvertDataTable<OwnerSummaryInfo>(ds.Tables[tableIndex]);
                    if (ownerSummaries != null && ownerSummaries.Count > 0)
                    {
                        objResponseInfo.objOwnerSummary = ownerSummaries[0];
                    }
                    tableIndex++;
                }

                // Table 2: MonthlyTrendInfo
                if (tableIndex < ds.Tables.Count)
                {
                    var monthlyTrends = _dbHelper.ConvertDataTable<MonthlyTrendInfo>(ds.Tables[tableIndex]);
                    if (monthlyTrends != null && monthlyTrends.Count > 0)
                    {
                        objResponseInfo.objMonthlyTrends = monthlyTrends;
                    }
                }
            }
            else if (hasTenantRole)
            {
                // Table 1: TenantSummaryInfo
                if (tableIndex < ds.Tables.Count)
                {
                    var tenantSummaries = _dbHelper.ConvertDataTable<TenantSummaryInfo>(ds.Tables[tableIndex]);
                    if (tenantSummaries != null && tenantSummaries.Count > 0)
                    {
                        objResponseInfo.objTenantSummary = tenantSummaries[0];
                    }
                }
            }

            return objResponseInfo;
        }

        public async Task<UserPropertyInfo> GetPropertyDetailsAsync(Guid userPublicId, Guid propertyPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetPropertyDetails);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<UserPropertyInfo> properties = _dbHelper.ConvertDataTable<UserPropertyInfo>(dt);

            if (properties == null || properties.Count == 0)
                return null;
            else
                return properties[0];
        }

        public async Task<UserPropertyMemberInfo> GetUserPropertyRole(Guid userPublicId, Guid propertyPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetUserPropertyRole);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<UserPropertyMemberInfo> roles = _dbHelper.ConvertDataTable<UserPropertyMemberInfo>(dt);
            if (roles == null || roles.Count == 0)
                return null;
            else
                return roles[0];
        }

        public async Task<UserPropertyResponseInfo> SavePropertyAsync(UserPropertyRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.sp_SaveProperty);
            cmd.CommandType = CommandType.StoredProcedure;

            var propertyPublicIdParam = new SqlParameter("@PropertyPublicId", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objUserPropertyInfo.PropertyPublicId ?? DBNull.Value
            };
            cmd.Parameters.Add(propertyPublicIdParam);

            cmd.Parameters.AddWithValue("@OwnerUserId",        objRequestInfo.objUserPropertyInfo.OwnerUserId);
            cmd.Parameters.AddWithValue("@PropertyCode",       (object?)objRequestInfo.objUserPropertyInfo.PropertyCode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PropertyName",       objRequestInfo.objUserPropertyInfo.PropertyName);
            cmd.Parameters.AddWithValue("@PropertyTypeId",     objRequestInfo.objUserPropertyInfo.PropertyTypeId);
            cmd.Parameters.AddWithValue("@Description",        (object?)objRequestInfo.objUserPropertyInfo.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AddressLine1",       objRequestInfo.objUserPropertyInfo.AddressLine1);
            cmd.Parameters.AddWithValue("@AddressLine2",       (object?)objRequestInfo.objUserPropertyInfo.AddressLine2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Landmark",           (object?)objRequestInfo.objUserPropertyInfo.Landmark ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CityId",             (object?)objRequestInfo.objUserPropertyInfo.CityId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@StateId",            (object?)objRequestInfo.objUserPropertyInfo.StateId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CountryId",          (object?)objRequestInfo.objUserPropertyInfo.CountryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pincode",            (object?)objRequestInfo.objUserPropertyInfo.Pincode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Latitude",           (object?)objRequestInfo.objUserPropertyInfo.Latitude ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Longitude",          (object?)objRequestInfo.objUserPropertyInfo.Longitude ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalFloors",        (object?)objRequestInfo.objUserPropertyInfo.TotalFloors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalUnits",         (object?)objRequestInfo.objUserPropertyInfo.TotalUnits ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalParkingSlots",  (object?)objRequestInfo.objUserPropertyInfo.TotalParkingSlots ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BuiltYear",          (object?)objRequestInfo.objUserPropertyInfo.BuiltYear ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsVerified",         objRequestInfo.objUserPropertyInfo.IsVerified);
            cmd.Parameters.AddWithValue("@IsPublicListing",    objRequestInfo.objUserPropertyInfo.IsPublicListing);
            cmd.Parameters.AddWithValue("@AllowPreBooking",    objRequestInfo.objUserPropertyInfo.AllowPreBooking);
            cmd.Parameters.AddWithValue("@IsActive",           objRequestInfo.objUserPropertyInfo.IsActive);
            cmd.Parameters.AddWithValue("@CoverImageUrl",          (object?)objRequestInfo.objUserPropertyInfo.CoverImageUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserPublicId",       objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            Guid? returnedPropertyPublicId = (Guid?)(propertyPublicIdParam.Value != DBNull.Value
                ? propertyPublicIdParam.Value
                : null);

            return new UserPropertyResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                PropertyPublicId = returnedPropertyPublicId
            };
        }
    }
}
