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

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            var properties = _dbHelper.ConvertDataTable<MyPropertyInfo>(dt);
            
            return properties;
        }

        public async Task<UserPropertyInfo> GetPropertyDetailsAsync(Guid userPublicId, long propertyId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetPropertyDetails);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<UserPropertyInfo> properties = _dbHelper.ConvertDataTable<UserPropertyInfo>(dt);

            if (properties == null || properties.Count == 0)
                return null;
            else
                return properties[0];
        }

        public async Task<UserPropertyMemberInfo> GetUserPropertyRole(Guid userPublicId, long propertyId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetUserPropertyRole);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

            var propertyIdParam = new SqlParameter("@PropertyId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objUserPropertyInfo.PropertyId ?? DBNull.Value
            };
            cmd.Parameters.Add(propertyIdParam);

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
            cmd.Parameters.AddWithValue("@UserPublicId",       objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            long? returnedPropertyId = propertyIdParam.Value != DBNull.Value
                ? Convert.ToInt64(propertyIdParam.Value)
                : null;

            return new UserPropertyResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                PropertyId = returnedPropertyId
            };
        }
    }
}
