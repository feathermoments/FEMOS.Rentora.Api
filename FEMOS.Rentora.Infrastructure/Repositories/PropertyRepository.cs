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

            var properties = new List<MyPropertyInfo>();
            foreach (DataRow row in dt.Rows)
            {
                properties.Add(new MyPropertyInfo
                {
                    PropertyId   = row.Table.Columns.Contains("PropertyId")   ? Convert.ToInt32(row["PropertyId"])        : 0,
                    PropertyName = row.Table.Columns.Contains("PropertyName") ? row["PropertyName"]?.ToString() ?? string.Empty : string.Empty,
                    PropertyType = row.Table.Columns.Contains("PropertyType") ? row["PropertyType"]?.ToString() ?? string.Empty : string.Empty,
                    City         = row.Table.Columns.Contains("City")         ? row["City"]?.ToString()         ?? string.Empty : string.Empty,
                    State        = row.Table.Columns.Contains("State")        ? row["State"]?.ToString()        ?? string.Empty : string.Empty,
                    AddressLine1 = row.Table.Columns.Contains("AddressLine1") ? row["AddressLine1"]?.ToString() ?? string.Empty : string.Empty,
                    TotalUnits   = row.Table.Columns.Contains("TotalUnits")   ? Convert.ToInt32(row["TotalUnits"])        : 0,
                    OccupiedUnits = row.Table.Columns.Contains("OccupiedUnits") ? Convert.ToInt32(row["OccupiedUnits"])   : 0,
                    VacantUnits  = row.Table.Columns.Contains("VacantUnits")  ? Convert.ToInt32(row["VacantUnits"])       : 0,
                    RoleId       = row.Table.Columns.Contains("RoleId")       ? Convert.ToInt32(row["RoleId"])            : 0,
                    RoleName     = row.Table.Columns.Contains("RoleName")     ? row["RoleName"]?.ToString()     ?? string.Empty : string.Empty,
                });
            }
            return properties;
        }

        public async Task<UserPropertyResponseInfo> SavePropertyAsync(UserPropertyRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.sp_SaveProperty);
            cmd.CommandType = CommandType.StoredProcedure;

            var propertyIdParam = new SqlParameter("@PropertyId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output,
                Value = (object?)objRequestInfo.objUserPropertyInfo.PropertyId ?? DBNull.Value
            };
            cmd.Parameters.Add(propertyIdParam);

            cmd.Parameters.AddWithValue("@OwnerUserId",        objRequestInfo.objUserPropertyInfo.OwnerUserId);
            cmd.Parameters.AddWithValue("@PropertyCode",       (object?)objRequestInfo.objUserPropertyInfo.PropertyCode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PropertyName",       objRequestInfo.objUserPropertyInfo.PropertyName);
            cmd.Parameters.AddWithValue("@PropertyTypeId",     objRequestInfo.objUserPropertyInfo.PropertyTypeId);
            cmd.Parameters.AddWithValue("@PropertyStatusId",   objRequestInfo.objUserPropertyInfo.PropertyStatusId);
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
            cmd.Parameters.AddWithValue("@IsDeleted",          objRequestInfo.objUserPropertyInfo.IsDeleted);
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
