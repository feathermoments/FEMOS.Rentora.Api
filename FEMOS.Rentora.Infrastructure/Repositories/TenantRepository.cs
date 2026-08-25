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
    internal class TenantRepository : ITenantRepository
    {
        private readonly IDBHelper _dbHelper;
        public TenantRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<List<MyPropertyTenantInfo>> GetPropertyTenantsAsync(Guid userPublicId, Guid propertyPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyTenant_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<MyPropertyTenantInfo>(dt);
        }

        public async Task<PropertyTenantInfo> GetPropertyTenantDetailsAsync(Guid userPublicId, Guid propertyPublicId, long tenantId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyTenant_Details);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@TenantId", tenantId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<PropertyTenantInfo> tenantDetails = _dbHelper.ConvertDataTable<PropertyTenantInfo>(dt);
            if (tenantDetails == null || tenantDetails.Count == 0)
            {
                return null;
            }
            else
                return tenantDetails[0];
        }

        public async Task<PropertyTenantResponseInfo> SavePropertyTenantAsync(PropertyTenantRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyTenant_Save);
            cmd.CommandType = CommandType.StoredProcedure;
            var tenantIdParam = new SqlParameter("@TenantId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objPropertyTenantInfo.TenantId ?? DBNull.Value
            };
            cmd.Parameters.Add(tenantIdParam);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.objPropertyTenantInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", objRequestInfo.objPropertyTenantInfo.UnitPublicId);
            cmd.Parameters.AddWithValue("@TenantUserId", objRequestInfo.objPropertyTenantInfo.TenantUserId);
            cmd.Parameters.AddWithValue("@TenantCode", objRequestInfo.objPropertyTenantInfo.TenantCode);
            cmd.Parameters.AddWithValue("@FullName", objRequestInfo.objPropertyTenantInfo.FullName);
            cmd.Parameters.AddWithValue("@EmailHash", objRequestInfo.objPropertyTenantInfo.EmailHash);
            cmd.Parameters.AddWithValue("@EmailEncrypted", objRequestInfo.objPropertyTenantInfo.EmailEncrypted);
            cmd.Parameters.AddWithValue("@MobileHash", objRequestInfo.objPropertyTenantInfo.MobileHash);
            cmd.Parameters.AddWithValue("@MobileEncrypted", objRequestInfo.objPropertyTenantInfo.MobileEncrypted);
            cmd.Parameters.AddWithValue("@GenderId", objRequestInfo.objPropertyTenantInfo.GenderId);
            cmd.Parameters.AddWithValue("@DateOfBirth", objRequestInfo.objPropertyTenantInfo.DateOfBirth);
            cmd.Parameters.AddWithValue("@Occupation", objRequestInfo.objPropertyTenantInfo.Occupation);
            cmd.Parameters.AddWithValue("@CompanyName", objRequestInfo.objPropertyTenantInfo.CompanyName);
            cmd.Parameters.AddWithValue("@PermanentAddress", objRequestInfo.objPropertyTenantInfo.PermanentAddress);
            cmd.Parameters.AddWithValue("@CompanyAddress", objRequestInfo.objPropertyTenantInfo.CompanyAddress);
            cmd.Parameters.AddWithValue("@EmergencyContactName", objRequestInfo.objPropertyTenantInfo.EmergencyContactName);
            cmd.Parameters.AddWithValue("@EmergencyContactNumber", objRequestInfo.objPropertyTenantInfo.EmergencyContactNumber);
            cmd.Parameters.AddWithValue("@Notes", objRequestInfo.objPropertyTenantInfo.Notes);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            long? tenantId = tenantIdParam.Value != DBNull.Value
                ? Convert.ToInt64(tenantIdParam.Value)
                : null;

            return new PropertyTenantResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                TenantId = tenantId
            };
        }

        public async Task<PropertyTenantAssignmentResponseInfo> SavePropertyTenantAssignmentAsync(PropertyTenantAssignmentRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.sp_SaveTenantAssignment);
            cmd.CommandType = CommandType.StoredProcedure;
            var tenantAssignmentPublicIdParam = new SqlParameter("@TenantAssignmentPublicId", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objTenantAssignmentInfo.TenantAssignmentPublicId ?? DBNull.Value
            };
            cmd.Parameters.Add(tenantAssignmentPublicIdParam);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.objTenantAssignmentInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", objRequestInfo.objTenantAssignmentInfo.UnitPublicId);
            cmd.Parameters.AddWithValue("@TenantId", objRequestInfo.objTenantAssignmentInfo.TenantId);
            cmd.Parameters.AddWithValue("@MoveInDate", objRequestInfo.objTenantAssignmentInfo.MoveInDate);
            cmd.Parameters.AddWithValue("@ExpectedMoveOutDate", objRequestInfo.objTenantAssignmentInfo.ExpectedMoveOutDate?.Year > 1900 ? objRequestInfo.objTenantAssignmentInfo.ExpectedMoveOutDate : DBNull.Value);
            cmd.Parameters.AddWithValue("@ActualMoveOutDate", objRequestInfo.objTenantAssignmentInfo.ActualMoveOutDate?.Year > 1900 ? objRequestInfo.objTenantAssignmentInfo.ActualMoveOutDate : DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentStatusId", objRequestInfo.objTenantAssignmentInfo.TenantAssignmentStatusId);
            cmd.Parameters.AddWithValue("@IsPrimaryTenant", objRequestInfo.objTenantAssignmentInfo.IsPrimaryTenant);
            cmd.Parameters.AddWithValue("@IsActive", objRequestInfo.objTenantAssignmentInfo.IsActive);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            Guid? tenantAssignmentPublicId = tenantAssignmentPublicIdParam.Value != DBNull.Value
                ? (Guid?)tenantAssignmentPublicIdParam.Value
                : null;

            return new PropertyTenantAssignmentResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                TenantAssignmentPublicId = tenantAssignmentPublicId
            };
        }
        
        public async Task<TenantAssignmentInfo> GetTenantAssignmentDetailsAsync(Guid userPublicId, Guid propertyPublicId, long tenantId, Guid tenantAssignmentPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_GetTenantAssignment);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@TenantId", tenantId);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", tenantAssignmentPublicId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<TenantAssignmentInfo> objTenantAssignments = _dbHelper.ConvertDataTable<TenantAssignmentInfo>(dt);
            if (objTenantAssignments == null || objTenantAssignments.Count == 0)
            {
                return null;
            }
            else
                return objTenantAssignments[0];
        }

        public async Task<List<TenantInfo>> SearchTenantAsync(Guid userPublicId, string searchText, string searchTextHash)
        {
            var cmd = new SqlCommand(DBConstants.usp_PropertyTenant_Search);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@SearchText", searchText);
            cmd.Parameters.AddWithValue("@SearchTextHash", searchTextHash);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<TenantInfo> objTenants = _dbHelper.ConvertDataTable<TenantInfo>(dt);
            return objTenants;
        }

        public async Task<BaseResponseInfo> DeletePropertyTenantAsync(Guid userPublicId, Guid propertyPublicId, long tenantId)
        {
            var cmd = new SqlCommand(DBConstants.usp_PropertyTenant_Delete);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@TenantId", tenantId);
            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);
            BaseResponseInfo baseResponseInfo = new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
            return baseResponseInfo;
        }

        public async Task<BaseResponseInfo> DeleteTenantAssignmentAsync(Guid userPublicId, Guid propertyPublicId, Guid tenantAssignmentPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_DeleteTenantAssignment);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", tenantAssignmentPublicId);
            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);
            BaseResponseInfo baseResponseInfo = new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
            return baseResponseInfo;
        }
    }
}
