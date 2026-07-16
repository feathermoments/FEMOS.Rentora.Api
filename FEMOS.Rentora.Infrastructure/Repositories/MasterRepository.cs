using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    internal class MasterRepository : IMasterRepository
    {
        private readonly IDBHelper _dbHelper;
        public MasterRepository(IDBHelper dbHelper) 
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<CityInfo>> GetCitiesByStateId(int stateId)
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetCitiesByStateId);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StateId", stateId);
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<CityInfo>(dt);
        }

        public async Task<List<StateInfo>> GetStatesByCountryId(int countryId)
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetStatesByCountryId);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CountryId", countryId);
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<StateInfo>(dt);
        }

        public async Task<List<CountryInfo>> GetCountries()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetCountries);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<CountryInfo>(dt);
        }

        public async Task<List<PropertyTypeInfo>> GetPropertyTypes()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetPropertyTypes);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return new List<PropertyTypeInfo>();
            }
            List<PropertyTypeInfo> propertyTypes = IDBHelper.ReferenceEquals(ds.Tables[0], null) ? new List<PropertyTypeInfo>() : _dbHelper.ConvertDataTable<PropertyTypeInfo>(ds.Tables[0]);
            List<PropertyUnitTypeInfo> propertyUnitTypes = IDBHelper.ReferenceEquals(ds.Tables[1], null) ? new List<PropertyUnitTypeInfo>() : _dbHelper.ConvertDataTable<PropertyUnitTypeInfo>(ds.Tables[1]);
            foreach (var propertyType in propertyTypes)
            {
                propertyType.objPropertyUnitTypes = propertyUnitTypes.Where(x => x.PropertyTypeId == propertyType.PropertyTypeId).ToList();
            }
            return propertyTypes;
        }

        public async Task<List<UnitTypeInfo>> GetUnitTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetUnitTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<UnitTypeInfo>(dt);
        }

        public async Task<List<BhkTypeInfo>> GetBHKTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetBHKTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<BhkTypeInfo>(dt);
        }

        public async Task<List<FurnishingTypeInfo>> GetFurnishingTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetFurnishingTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<FurnishingTypeInfo>(dt);
        }

        public async Task<List<UnitStatusTypeInfo>> GetUnitStatusTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetUnitStatusTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<UnitStatusTypeInfo>(dt);
        }

        public async Task<List<PropertyStatusTypeInfo>> GetPropertyStatusTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetPropertyStatusTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<PropertyStatusTypeInfo>(dt);
        }

        public async Task<List<AgreementStatusTypeInfo>> GetAgreementStatusTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetAgreementStatusTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<AgreementStatusTypeInfo>(dt);
        }

        public async Task<List<TenantAssignmentStatusTypeInfo>> GetTenantAssignmentStatusTypesAsync()
        {
            var cmd = new SqlCommand(DBConstants.sp_Mst_GetTenantAssignmentStatusTypes);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<TenantAssignmentStatusTypeInfo>(dt);
        }
    }
}
