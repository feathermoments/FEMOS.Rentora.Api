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
    internal class SettlementRepository : ISettlementRepository
    {
        private readonly IDBHelper _dbHelper;
        public SettlementRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<MoveOutSettlementResponseInfo> CreateSettlementAsync(MoveOutSettlementRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Create);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentAgreementId", objRequestInfo.objSettlementInfo.RentAgreementId);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", objRequestInfo.objSettlementInfo.TenantAssignmentId);
            cmd.Parameters.AddWithValue("@SettlementDate", objRequestInfo.objSettlementInfo.SettlementDate);
            cmd.Parameters.AddWithValue("@OutstandingRent", objRequestInfo.objSettlementInfo.OutstandingRent);
            cmd.Parameters.AddWithValue("@OutstandingMaintenance", objRequestInfo.objSettlementInfo.OutstandingMaintenance);
            cmd.Parameters.AddWithValue("@UtilityCharges", objRequestInfo.objSettlementInfo.UtilityCharges);
            cmd.Parameters.AddWithValue("@DamageCharges", objRequestInfo.objSettlementInfo.DamageCharges);
            cmd.Parameters.AddWithValue("@LateFee", objRequestInfo.objSettlementInfo.LateFee);
            cmd.Parameters.AddWithValue("@OtherCharges", objRequestInfo.objSettlementInfo.OtherCharges);
            cmd.Parameters.AddWithValue("@SecurityDepositHeld", objRequestInfo.objSettlementInfo.SecurityDepositHeld);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.objSettlementInfo.Remarks) ? DBNull.Value : objRequestInfo.objSettlementInfo.Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new MoveOutSettlementResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<MoveOutSettlementResponseInfo> GetSettlementDetailsAsync(Guid userPublicId, Guid uniqueId)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Details);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UniqueId", uniqueId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            MoveOutSettlementResponseInfo objResponseInfo = new MoveOutSettlementResponseInfo();

            if (dt != null && dt.Rows.Count > 0)
            {
                List<MoveOutSettlementInfo> objSettlements = _dbHelper.ConvertDataTable<MoveOutSettlementInfo>(dt);
                objResponseInfo.objSettlementInfo = objSettlements.FirstOrDefault();
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Settlement details retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Settlement not found.";
            }

            return objResponseInfo;
        }

        public async Task<FilterResponseInfo> GetPendingActionsAsync(FilterRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_GetPendingActions);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UniqueId", objRequestInfo.objFilterInfo.UniqueId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return new FilterResponseInfo()
            {
                Status = StatusConstants.Success,
                Message = "Pending actions retrieved successfully.",
                objFilterData = dt != null ? _dbHelper.ConvertDataTable<SettlementPendingActionInfo>(dt) : new List<SettlementPendingActionInfo>()
            };
        }

        public async Task<FilterResponseInfo> GetSettlementsAsync(FilterRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentAgreementId", (object?)objRequestInfo.objFilterInfo.RentAgreementId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", (object?)objRequestInfo.objFilterInfo.TenantAssignmentId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SettlementStatusId", (object?)objRequestInfo.objFilterInfo.SettlementStatusId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", (object?)objRequestInfo.objFilterInfo.FromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object?)objRequestInfo.objFilterInfo.ToDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PropertyId", objRequestInfo.objFilterInfo.PropertyId);
            cmd.Parameters.AddWithValue("@UnitId", objRequestInfo.objFilterInfo.UnitId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<MoveOutSettlementInfo> objSettlements = _dbHelper.ConvertDataTable<MoveOutSettlementInfo>(dt);
            return new FilterResponseInfo()
            {
                Status = StatusConstants.Success,
                Message = "Settlements retrieved successfully.",
                objFilterData = objSettlements
            };
        }

        public async Task<BaseResponseInfo> CreatePaymentAsync(MoveOutSettlementCreateRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Payment_Create);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UniqueId", objRequestInfo.UniqueId);
            cmd.Parameters.AddWithValue("@PaymentMethodId", objRequestInfo.objRentPaymentInfo.PaymentMethodId);
            cmd.Parameters.AddWithValue("@TransactionReferenceNo", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.TransactionReferenceNo) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.TransactionReferenceNo);
            cmd.Parameters.AddWithValue("@PaymentGatewayTransactionId", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.PaymentGatewayTransactionId) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.PaymentGatewayTransactionId);
            cmd.Parameters.AddWithValue("@GatewayName", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.GatewayName) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.GatewayName);
            cmd.Parameters.AddWithValue("@GatewayResponse", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.GatewayResponse) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.GatewayResponse);
            cmd.Parameters.AddWithValue("@IsOnlinePayment", objRequestInfo.objRentPaymentInfo.IsOnlinePayment);
            cmd.Parameters.AddWithValue("@ReferenceNumber", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.ReferenceNumber) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.ReferenceNumber);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.Remarks) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.Remarks);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> ApprovePaymentAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Payment_Approve);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentPaymentId", objRequestInfo.RentPaymentId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.Remarks) ? DBNull.Value : objRequestInfo.Remarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> RejectPaymentAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Payment_Reject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentPaymentId", objRequestInfo.RentPaymentId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.Remarks) ? DBNull.Value : objRequestInfo.Remarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> ConfirmRefundAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Refund_Confirm);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentPaymentId", objRequestInfo.RentPaymentId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.Remarks) ? DBNull.Value : objRequestInfo.Remarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> MarkRefundPaidAsync(MoveOutSettlementCreateRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Refund_MarkPaid);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UniqueId", objRequestInfo.UniqueId);
            cmd.Parameters.AddWithValue("@PaymentMethodId", objRequestInfo.objRentPaymentInfo.PaymentMethodId);
            cmd.Parameters.AddWithValue("@TransactionReferenceNo", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.TransactionReferenceNo) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.TransactionReferenceNo);
            cmd.Parameters.AddWithValue("@PaymentGatewayTransactionId", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.PaymentGatewayTransactionId) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.PaymentGatewayTransactionId);
            cmd.Parameters.AddWithValue("@GatewayName", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.GatewayName) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.GatewayName);
            cmd.Parameters.AddWithValue("@GatewayResponse", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.GatewayResponse) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.GatewayResponse);
            cmd.Parameters.AddWithValue("@IsOnlinePayment", objRequestInfo.objRentPaymentInfo.IsOnlinePayment);
            cmd.Parameters.AddWithValue("@ReferenceNumber", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.ReferenceNumber) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.ReferenceNumber);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.objRentPaymentInfo.Remarks) ? DBNull.Value : objRequestInfo.objRentPaymentInfo.Remarks);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> RejectRefundAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Refund_Reject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentPaymentId", objRequestInfo.RentPaymentId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objRequestInfo.Remarks) ? DBNull.Value : objRequestInfo.Remarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> ApproveSettlementAsync(Guid uniqueId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_MoveOutSettlement_Approve);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UniqueId", uniqueId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo()
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }
    }
}
