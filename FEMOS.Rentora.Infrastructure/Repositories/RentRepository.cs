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

        public async Task<FilterRentInvoiceResponseInfo> GetRentInvoicesAsync(FilterRentInvoiceRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_RentInvoice_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", objRequestInfo.objFilterInfo.PropertyId);
            cmd.Parameters.AddWithValue("@UnitId", (object?)objRequestInfo.objFilterInfo.UnitId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", (object?)objRequestInfo.objFilterInfo.TenantAssignmentId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RentAgreementId", (object?)objRequestInfo.objFilterInfo.RentAgreementId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceStatusId", (object?)objRequestInfo.objFilterInfo.InvoiceStatusId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PaymentStatusId", (object?)objRequestInfo.objFilterInfo.PaymentStatusId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BillingYear", (object?)objRequestInfo.objFilterInfo.BillingYear ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BillingMonth", (object?)objRequestInfo.objFilterInfo.BillingMonth ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDueDate", (object?)objRequestInfo.objFilterInfo.FromDueDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDueDate", (object?)objRequestInfo.objFilterInfo.ToDueDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OutstandingOnly", objRequestInfo.objFilterInfo.OutstandingOnly);
            cmd.Parameters.AddWithValue("@OverDueOnly", objRequestInfo.objFilterInfo.OverDueOnly);
            cmd.Parameters.AddWithValue("@SearchText", (object?)objRequestInfo.objFilterInfo.SearchText ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PageNumber", objRequestInfo.objFilterInfo.PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", objRequestInfo.objFilterInfo.PageSize);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<RentInvoiceInfo> objRentInvoices = _dbHelper.ConvertDataTable<RentInvoiceInfo>(dt);
            return new FilterRentInvoiceResponseInfo()
            {
                Status = "Success",
                Message = "Rent invoices retrieved successfully.",
                objRentInvoices = objRentInvoices
            };
        }

        public async Task<RentInvoiceResponseInfo> GetRentInvoiceDetailsAsync(Guid userPublicId, long propertyId, long rentInvoiceId)
        {
            var cmd = new SqlCommand(DBConstants.USP_RentInvoice_GetDetails);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            cmd.Parameters.AddWithValue("@RentInvoiceId", rentInvoiceId);
            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);
            List<RentInvoiceInfo> objRentInvoices = _dbHelper.ConvertDataTable<RentInvoiceInfo>(ds.Tables[0]);
            if (objRentInvoices != null && objRentInvoices.Count > 0)
            {
                RentInvoiceResponseInfo objResponseInfo = new RentInvoiceResponseInfo()
                {
                    objRentInvoiceInfo = objRentInvoices[0],
                    objRentAgreementInfo = _dbHelper.ConvertDataTable<RentAgreementInfo>(ds.Tables[1])?.FirstOrDefault(),
                    objPropertyUnitInfo = _dbHelper.ConvertDataTable<PropertyUnitInfo>(ds.Tables[2])?.FirstOrDefault(),
                    objPropertyOwnerInfo = _dbHelper.ConvertDataTable<PropertyMemberInfo>(ds.Tables[3])?.FirstOrDefault(),
                    objPropertyTenantInfo = _dbHelper.ConvertDataTable<PropertyMemberInfo>(ds.Tables[4])?.FirstOrDefault(),
                    obRentPaymentInfo = _dbHelper.ConvertDataTable<RentPaymentInfo>(ds.Tables[5])?.FirstOrDefault(),
                    objRentPaymentSummaryInfo = _dbHelper.ConvertDataTable<RentPaymentSummaryInfo>(ds.Tables[6])?.FirstOrDefault(),
                    Status = StatusConstants.Success,
                    Message = "Rent invoice details retrieved successfully."
                };
                return objResponseInfo;
            }
            else
            {
                RentInvoiceResponseInfo objResponseInfo = new RentInvoiceResponseInfo()
                {
                    Status = "Failed",
                    Message = "Rent invoice not found.",
                };
                return objResponseInfo;
            }
        }

        public async Task<RentPaymentResponseInfo> SaveRentPaymentAsync(RentPaymentRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_RentPayment_Save);
            cmd.CommandType = CommandType.StoredProcedure;
            var transactionGuidParam = new SqlParameter("@TransactionGuid", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.obRentPaymentInfo.TransactionGuid ?? DBNull.Value
            };
            cmd.Parameters.Add(transactionGuidParam);

            DataTable tvp = new DataTable();
            tvp.Columns.Add("RentInvoiceId", typeof(long));
            tvp.Columns.Add("OutstandingAmount", typeof(decimal));
            tvp.Columns.Add("PaidAmount", typeof(decimal));

            foreach (var item in objRequestInfo.obRentPaymentInfo.Invoices)
            {
                tvp.Rows.Add(item.RentInvoiceId, item.OutstandingAmount, item.PaidAmount);
            }

            SqlParameter paymentInvoicesParam = new SqlParameter("@PaymentInvoices", SqlDbType.Structured);
            paymentInvoicesParam.TypeName = "dbo.TVP_RentPaymentInvoice";
            paymentInvoicesParam.Value = tvp;
            cmd.Parameters.Add(paymentInvoicesParam);

            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PaymentAmount", objRequestInfo.obRentPaymentInfo.PaymentAmount);
            cmd.Parameters.AddWithValue("@PaymentMethodId", objRequestInfo.obRentPaymentInfo.PaymentMethodId);
            cmd.Parameters.AddWithValue("@PaymentDate", objRequestInfo.obRentPaymentInfo.PaymentDate);
            cmd.Parameters.AddWithValue("@TransactionReferenceNo", objRequestInfo.obRentPaymentInfo.TransactionReferenceNo);
            cmd.Parameters.AddWithValue("@ReferenceNumber", objRequestInfo.obRentPaymentInfo.ReferenceNumber);
            cmd.Parameters.AddWithValue("@GatewayName", objRequestInfo.obRentPaymentInfo.GatewayName);
            cmd.Parameters.AddWithValue("@GatewayTransactionId", objRequestInfo.obRentPaymentInfo.GatewayTransactionId);
            cmd.Parameters.AddWithValue("@GatewayResponse", objRequestInfo.obRentPaymentInfo.GatewayResponse);
            cmd.Parameters.AddWithValue("@IsOnlinePayment", objRequestInfo.obRentPaymentInfo.IsOnlinePayment);
            cmd.Parameters.AddWithValue("@Remarks", objRequestInfo.obRentPaymentInfo.Remarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            Guid? transactionGuid = transactionGuidParam.Value != DBNull.Value
                ? (Guid?)transactionGuidParam.Value
                : null;

            return new RentPaymentResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                TransactionGuid = transactionGuid
            };
        }
    }
}