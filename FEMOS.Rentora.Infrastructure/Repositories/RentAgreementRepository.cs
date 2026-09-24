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

        public async Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, Guid RentAgreementPublicId, Guid TenantAssignmentPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_RentAgreement_Delete);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", RentAgreementPublicId);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", TenantAssignmentPublicId);
            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);
            BaseResponseInfo baseResponseInfo = new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
            return baseResponseInfo;
        }

        public async Task<RentAgreementInfo> GetRentAgreementAsync(Guid userPublicId, Guid TenantAssignmentPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_RentAgreement_Details);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", TenantAssignmentPublicId);
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
            var rentAgreementPublicIdParam = new SqlParameter("@RentAgreementPublicId", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objRentAgreementInfo.RentAgreementPublicId ?? DBNull.Value
            };
            cmd.Parameters.Add(rentAgreementPublicIdParam);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", objRequestInfo.objRentAgreementInfo.TenantAssignmentPublicId);
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
            cmd.Parameters.AddWithValue("@PreviousRentAgreementPublicId", objRequestInfo.objRentAgreementInfo.PreviousRentAgreementPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            Guid? rentAgreementPublicId = rentAgreementPublicIdParam.Value != DBNull.Value
                ? (Guid?)rentAgreementPublicIdParam.Value
                : null;

            return new RentAgreementResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                RentAgreementPublicId = rentAgreementPublicId
            };
        }

        public async Task<FilterResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.objFilterInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", (object?)objRequestInfo.objFilterInfo.UnitPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantId", (object?)objRequestInfo.objFilterInfo.TenantId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", (object?)objRequestInfo.objFilterInfo.TenantAssignmentPublicId ?? DBNull.Value);
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
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", objRequestInfo.objTerminationRequestInfo.RentAgreementPublicId);
            cmd.Parameters.AddWithValue("@TerminationDate", objRequestInfo.objTerminationRequestInfo.TerminationDate);
            cmd.Parameters.AddWithValue("@Reason", string.IsNullOrEmpty(objRequestInfo.objTerminationRequestInfo.Reason) ? DBNull.Value : objRequestInfo.objTerminationRequestInfo.Reason);
            cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(objRequestInfo.objTerminationRequestInfo.Notes) ? DBNull.Value : objRequestInfo.objTerminationRequestInfo.Notes);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }

        public async Task<FilterResponseInfo> GetTerminationRequestsAsync(FilterRequestInfo objRequestInfo)
        {
            FilterResponseInfo objResponseInfo = new FilterResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", (object?)objRequestInfo.objFilterInfo.RentAgreementPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentPublicId", (object?)objRequestInfo.objFilterInfo.TenantAssignmentPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TerminationRequestStatusId", (object?)objRequestInfo.objFilterInfo.TerminationRequestStatusId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", (object?)objRequestInfo.objFilterInfo.FromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object?)objRequestInfo.objFilterInfo.ToDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PageNumber", objRequestInfo.objFilterInfo.PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", objRequestInfo.objFilterInfo.PageSize);
            cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.objFilterInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", objRequestInfo.objFilterInfo.UnitPublicId);

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

        public async Task<RentAgreementTerminationRequestResponseInfo> GetTerminationRequestDetailsAsync(Guid userPublicId, Guid rentAgreementPublicId, Guid terminationRequestPublicId)
        {
            RentAgreementTerminationRequestResponseInfo objResponseInfo = new RentAgreementTerminationRequestResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Get);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", rentAgreementPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", terminationRequestPublicId);

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

        public async Task<BaseResponseInfo> ApproveTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Approve);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", objRequestInfo.RentAgreementPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", objRequestInfo.TerminationRequestPublicId);
            cmd.Parameters.AddWithValue("@ActionRemarks", string.IsNullOrEmpty(objRequestInfo.ActionRemarks) ? DBNull.Value : objRequestInfo.ActionRemarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> RejectTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Reject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", objRequestInfo.RentAgreementPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", objRequestInfo.TerminationRequestPublicId);
            cmd.Parameters.AddWithValue("@ActionRemarks", string.IsNullOrEmpty(objRequestInfo.ActionRemarks) ? DBNull.Value : objRequestInfo.ActionRemarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> CancelTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentAgreement_TerminationRequest_Cancel);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", objRequestInfo.RentAgreementPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", objRequestInfo.TerminationRequestPublicId);
            cmd.Parameters.AddWithValue("@ActionRemarks", string.IsNullOrEmpty(objRequestInfo.ActionRemarks) ? DBNull.Value : objRequestInfo.ActionRemarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            objResponseInfo.Status = dbResponse.Status;
            objResponseInfo.Message = dbResponse.Message;

            return objResponseInfo;
        }
    }
}
