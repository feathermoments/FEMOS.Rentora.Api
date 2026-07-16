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
    internal class RentRepository : IRentRepository
    {
        private readonly IDBHelper _dbHelper;
        public RentRepository(IDBHelper dbHelper) 
        { 
            _dbHelper = dbHelper;
        }

        public async Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, long RentAgreementId, long TenantAssignmentId)
        {
            var cmd = new SqlCommand(DBConstants.usp_DeleteRentAgreement);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementId", RentAgreementId);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", TenantAssignmentId);
            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);
            BaseResponseInfo baseResponseInfo = new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
            return baseResponseInfo;
        }

        public async Task<RentAgreementInfo> GetRentAgreementAsync(Guid userPublicId, long TenantAssignmentId)
        {
            var cmd = new SqlCommand(DBConstants.usp_GetRentAgreement);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", TenantAssignmentId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<RentAgreementInfo> objRentAgreements = _dbHelper.ConvertDataTable<RentAgreementInfo>(dt);
            if (objRentAgreements == null || objRentAgreements.Count == 0)
            {
                return null;
            }
            else
                return objRentAgreements[0];
        }

        public async Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.usp_SaveRentAgreement);
            cmd.CommandType = CommandType.StoredProcedure;
            var rentAgreementIdParam = new SqlParameter("@RentAgreementId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objRentAgreementInfo.RentAgreementId ?? DBNull.Value
            };
            cmd.Parameters.Add(rentAgreementIdParam);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", objRequestInfo.objRentAgreementInfo.TenantAssignmentId);
            cmd.Parameters.AddWithValue("@AgreementNumber", objRequestInfo.objRentAgreementInfo.AgreementNumber);
            cmd.Parameters.AddWithValue("@StartDate", objRequestInfo.objRentAgreementInfo.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", objRequestInfo.objRentAgreementInfo.EndDate);
            cmd.Parameters.AddWithValue("@MonthlyRent", objRequestInfo.objRentAgreementInfo.MonthlyRent);
            cmd.Parameters.AddWithValue("@SecurityDeposit", objRequestInfo.objRentAgreementInfo.SecurityDeposit);
            cmd.Parameters.AddWithValue("@MaintenanceAmount", objRequestInfo.objRentAgreementInfo.MaintenanceAmount);
            cmd.Parameters.AddWithValue("@RentDueDay", objRequestInfo.objRentAgreementInfo.RentDueDay);
            cmd.Parameters.AddWithValue("@NoticePeriodDays", objRequestInfo.objRentAgreementInfo.NoticePeriodDays);
            cmd.Parameters.AddWithValue("@AgreementStatusId", objRequestInfo.objRentAgreementInfo.AgreementStatusId);
            cmd.Parameters.AddWithValue("@AgreementDocumentUrl", objRequestInfo.objRentAgreementInfo.AgreementDocumentUrl);
            cmd.Parameters.AddWithValue("@IsActive", objRequestInfo.objRentAgreementInfo.IsActive);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            long? rentAgreementId = rentAgreementIdParam.Value != DBNull.Value
                ? Convert.ToInt64(rentAgreementIdParam.Value)
                : null;

            return new RentAgreementResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                RentAgreementId = rentAgreementId
            };
        }
    }
}
