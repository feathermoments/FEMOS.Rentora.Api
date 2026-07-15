using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Infrastructure.Persistance;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    internal class UnitRepository : IUnitRepository
    {
        private readonly IDBHelper _dbHelper;

        public UnitRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<PropertyUnitInfo> GetPropertyUnitDetailsAsync(Guid userPublicId, long propertyId, long unitId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyUnit_GetById);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            cmd.Parameters.AddWithValue("@UnitId", unitId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<PropertyUnitInfo> propertyUnits = _dbHelper.ConvertDataTable<PropertyUnitInfo>(dt);

            if (propertyUnits == null || propertyUnits.Count == 0)
                return null;
            else
                return propertyUnits[0];
        }

        public async Task<List<MyPropertyUnitInfo>> GetPropertyUnitsAsync(Guid userPublicId, long propertyId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyUnit_GetAll);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<MyPropertyUnitInfo>(dt);
        }

        public async Task<List<MyPropertyUnitInfo>> GetVacantUnitsAsync(Guid userPublicId, long propertyId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyUnit_GetVacantUnits);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<MyPropertyUnitInfo>(dt); 
        }

        public async Task<PropertyUnitResponseInfo> SavePropertyUnitAsync(PropertyUnitRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyUnit_Save);
            cmd.CommandType = CommandType.StoredProcedure;

            var unitIdParam = new SqlParameter("@UnitId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objPropertyUnit.UnitId ?? DBNull.Value
            };
            cmd.Parameters.Add(unitIdParam);

            cmd.Parameters.AddWithValue("@PropertyId", objRequestInfo.objPropertyUnit.PropertyId);
            cmd.Parameters.AddWithValue("@UnitNumber", objRequestInfo.objPropertyUnit.UnitNumber);
            cmd.Parameters.AddWithValue("@FloorNo", objRequestInfo.objPropertyUnit.FloorNo);
            cmd.Parameters.AddWithValue("@UnitTypeId", objRequestInfo.objPropertyUnit.UnitTypeId);
            cmd.Parameters.AddWithValue("@BHKTypeId", objRequestInfo.objPropertyUnit.BHKTypeId);
            cmd.Parameters.AddWithValue("@AreaSqFt", objRequestInfo.objPropertyUnit.AreaSqFt);
            cmd.Parameters.AddWithValue("@BedroomCount", objRequestInfo.objPropertyUnit.BedroomCount);
            cmd.Parameters.AddWithValue("@BathroomCount", objRequestInfo.objPropertyUnit.BathroomCount);
            cmd.Parameters.AddWithValue("@BalconyCount", objRequestInfo.objPropertyUnit.BalconyCount);
            cmd.Parameters.AddWithValue("@FurnishingTypeId", objRequestInfo.objPropertyUnit.FurnishingTypeId);
            cmd.Parameters.AddWithValue("@MonthlyRent", objRequestInfo.objPropertyUnit.MonthlyRent);
            cmd.Parameters.AddWithValue("@SecurityDeposit", objRequestInfo.objPropertyUnit.SecurityDeposit);
            cmd.Parameters.AddWithValue("@MaintenanceAmount", objRequestInfo.objPropertyUnit.MaintenanceAmount);
            cmd.Parameters.AddWithValue("@ElectricityMeterNo", objRequestInfo.objPropertyUnit.ElectricityMeterNo);
            cmd.Parameters.AddWithValue("@WaterMeterNo", objRequestInfo.objPropertyUnit.WaterMeterNo);
            cmd.Parameters.AddWithValue("@IsParkingIncluded", objRequestInfo.objPropertyUnit.IsParkingIncluded);
            cmd.Parameters.AddWithValue("@UnitStatusId", objRequestInfo.objPropertyUnit.UnitStatusId);
            cmd.Parameters.AddWithValue("@IsAvailable", objRequestInfo.objPropertyUnit.IsAvailable);
            cmd.Parameters.AddWithValue("@AvailableFrom", objRequestInfo.objPropertyUnit.AvailableFrom);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            long? returnedUnitId = unitIdParam.Value != DBNull.Value
                ? Convert.ToInt64(unitIdParam.Value)
                : null;

            return new PropertyUnitResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                UnitId = returnedUnitId
            };
        }
    }
}
