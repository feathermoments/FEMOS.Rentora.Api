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
    internal class RentAgreementRepository : IRentAgreementRepository
    {
        private readonly IDBHelper _dbHelper;
        public RentAgreementRepository(IDBHelper dbHelper)
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
            var cmd = new SqlCommand(DBConstants.usp_RentAgreement_Details);
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
            var cmd = new SqlCommand(DBConstants.usp_RentAgreement_Save);
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
            cmd.Parameters.AddWithValue("@BillingCycleTypeId", objRequestInfo.objRentAgreementInfo.BillingCycleTypeId);
            cmd.Parameters.AddWithValue("@ProrationTypeId", objRequestInfo.objRentAgreementInfo.ProrationTypeId);
            cmd.Parameters.AddWithValue("@BillingCycleStartDay", objRequestInfo.objRentAgreementInfo.BillingCycleStartDay);
            cmd.Parameters.AddWithValue("@PreviousRentAgreementId", objRequestInfo.objRentAgreementInfo.PreviousRentAgreementId);

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

        public async Task<FilterResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", objRequestInfo.objFilterInfo.PropertyId);
            cmd.Parameters.AddWithValue("@UnitId", (object?)objRequestInfo.objFilterInfo.UnitId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantId", (object?)objRequestInfo.objFilterInfo.TenantId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", (object?)objRequestInfo.objFilterInfo.TenantAssignmentId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SearchText", (object?)objRequestInfo.objFilterInfo.SearchText ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PageNumber", objRequestInfo.objFilterInfo.PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", objRequestInfo.objFilterInfo.PageSize);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<RentAgreementInfo> objRentAgreements = _dbHelper.ConvertDataTable<RentAgreementInfo>(dt);
            return new FilterResponseInfo()
            {
                Status = "Success",
                Message = "Rent agreements retrieved successfully.",
                objFilterData = objRentAgreements
            };
        }

        public async Task<RentAgreementTerminationRequestResponseInfo> CreateTerminationRequestAsync(CreateRentAgreementTerminationRequestInfo objRequestInfo)
        {
            RentAgreementTerminationRequestResponseInfo objResponseInfo = new RentAgreementTerminationRequestResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Create);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentAgreementId", objRequestInfo.objTerminationRequestInfo.RentAgreementId);
            cmd.Parameters.AddWithValue("@TerminationDate", objRequestInfo.objTerminationRequestInfo.TerminationDate);
            cmd.Parameters.AddWithValue("@Reason", string.IsNullOrEmpty(objRequestInfo.objTerminationRequestInfo.Reason) ? DBNull.Value : objRequestInfo.objTerminationRequestInfo.Reason);
            cmd.Parameters.AddWithValue("@RequestedByUserId", objRequestInfo.objTerminationRequestInfo.RequestedByUserId);
            cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(objRequestInfo.objTerminationRequestInfo.Notes) ? DBNull.Value : objRequestInfo.objTerminationRequestInfo.Notes);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            if (dbResponse.Status == StatusConstants.Success)
            {
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Termination request created successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = dbResponse.Message;
            }

            return objResponseInfo;
        }

        public async Task<FilterResponseInfo> GetTerminationRequestsAsync(FilterRequestInfo objRequestInfo)
        {
            FilterResponseInfo objResponseInfo = new FilterResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementId", (object?)objRequestInfo.objFilterInfo.RentAgreementId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", (object?)objRequestInfo.objFilterInfo.TenantAssignmentId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TerminationRequestStatusId", (object?)objRequestInfo.objFilterInfo.TerminationRequestStatusId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RequestedByUserId", (object?)objRequestInfo.objFilterInfo.RequestedByUserId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", (object?)objRequestInfo.objFilterInfo.FromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object?)objRequestInfo.objFilterInfo.ToDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PageNumber", objRequestInfo.objFilterInfo.PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", objRequestInfo.objFilterInfo.PageSize);

            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                objResponseInfo.objFilterData = _dbHelper.ConvertDataTable<RentAgreementTerminationRequestInfo>(dt);
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Termination requests retrieved successfully.";
            }
            else
            {
                objResponseInfo.objFilterData = new List<RentAgreementTerminationRequestInfo>();
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "No termination requests found.";
            }

            return objResponseInfo;
        }

        public async Task<RentAgreementTerminationRequestResponseInfo> GetTerminationRequestDetailsAsync(Guid userPublicId, Guid terminationRequestUniqueId)
        {
            RentAgreementTerminationRequestResponseInfo objResponseInfo = new RentAgreementTerminationRequestResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Get);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@TerminationRequestUniqueId", terminationRequestUniqueId);

            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                var terminationRequest = _dbHelper.ConvertDataTable<RentAgreementTerminationRequestInfo>(dt).FirstOrDefault();
                objResponseInfo.objTerminationRequestInfo = terminationRequest;
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Termination request details retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Termination request not found.";
            }

            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> ApproveTerminationRequestAsync(Guid userPublicId, Guid terminationRequestUniqueId, string actionRemarks)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Approve);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", terminationRequestUniqueId);
            cmd.Parameters.AddWithValue("@ActionRemarks", string.IsNullOrEmpty(actionRemarks) ? DBNull.Value : actionRemarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> RejectTerminationRequestAsync(Guid userPublicId, Guid terminationRequestUniqueId, string actionRemarks)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Reject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", terminationRequestUniqueId);
            cmd.Parameters.AddWithValue("@ActionRemarks", string.IsNullOrEmpty(actionRemarks) ? DBNull.Value : actionRemarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> CancelTerminationRequestAsync(Guid userPublicId, Guid terminationRequestUniqueId, string actionRemarks)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Cancel);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", terminationRequestUniqueId);
            cmd.Parameters.AddWithValue("@ActionRemarks", string.IsNullOrEmpty(actionRemarks) ? DBNull.Value : actionRemarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }
    }
}
