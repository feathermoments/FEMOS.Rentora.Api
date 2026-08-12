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
    internal class UtilityRepository : IUtilityRepository
    {
        private readonly IDBHelper _dbHelper;
        public UtilityRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<UtilityChargeResponseInfo> SaveUtilityChargeAsync(UtilityChargeRequestInfo objRequestInfo)
        {
            UtilityChargeResponseInfo objResponseInfo = new UtilityChargeResponseInfo();
            var objUtilityCharge = objRequestInfo.objUtilityChargeInfo;

            var cmd = new SqlCommand(DBConstants.USP_RentUtilityCharge_Save);
            cmd.CommandType = CommandType.StoredProcedure;
            var userIdParam = new SqlParameter("@UniqueId", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.objUtilityChargeInfo.UniqueId ?? Guid.Empty
            };
            cmd.Parameters.Add(userIdParam);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@RentInvoiceId", objUtilityCharge.RentInvoiceId);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", objUtilityCharge.TenantAssignmentId);
            cmd.Parameters.AddWithValue("@UtilityTypeId", objUtilityCharge.UtilityTypeId);
            cmd.Parameters.AddWithValue("@ChargeDate", objUtilityCharge.ChargeDate);
            cmd.Parameters.AddWithValue("@PreviousReading", objUtilityCharge.PreviousReading);
            cmd.Parameters.AddWithValue("@CurrentReading", objUtilityCharge.CurrentReading);
            //cmd.Parameters.AddWithValue("@UnitsConsumed", objUtilityCharge.UnitsConsumed);
            cmd.Parameters.AddWithValue("@RatePerUnit", objUtilityCharge.RatePerUnit);
            cmd.Parameters.AddWithValue("@FixedCharge", objUtilityCharge.FixedCharge);
            //cmd.Parameters.AddWithValue("@TotalCharge", objUtilityCharge.TotalCharge);
            cmd.Parameters.AddWithValue("@TaxAmount", objUtilityCharge.TaxAmount);
            cmd.Parameters.AddWithValue("@DiscountAmount", objUtilityCharge.DiscountAmount);
            //cmd.Parameters.AddWithValue("@TotalAmount", objUtilityCharge.TotalAmount);
            cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(objUtilityCharge.Remarks) ? DBNull.Value : objUtilityCharge.Remarks);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            Guid? UniqueId = userIdParam.Value != DBNull.Value
                ? (Guid?)userIdParam.Value
                : null;

            if (!string.IsNullOrEmpty(result))
            {
                objResponseInfo.UtilityChargeUniqueId = UniqueId ?? Guid.Empty;
                objResponseInfo.Status = dbResponse.Status;
                objResponseInfo.Message = dbResponse.Message;
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Failed to save utility charge.";
            }

            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> DeleteUtilityChargeAsync(Guid userPublicId, Guid utilityChargeUniqueId)
        {
            BaseResponseInfo objResponseInfo = new BaseResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentUtilityCharge_Delete);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", utilityChargeUniqueId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            if (!string.IsNullOrEmpty(result))
            {
                objResponseInfo.Status = dbResponse.Status;
                objResponseInfo.Message = dbResponse.Message;
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Failed to delete utility charge.";
            }

            return objResponseInfo;
        }

        public async Task<UtilityChargeResponseInfo> GetUtilityChargeDetailsAsync(Guid userPublicId, Guid utilityChargeUniqueId)
        {
            UtilityChargeResponseInfo objResponseInfo = new UtilityChargeResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentUtilityCharge_Details);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@UniqueId", utilityChargeUniqueId);

            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                var utilityCharge = _dbHelper.ConvertDataTable<UtilityChargeInfo>(dt).FirstOrDefault();
                objResponseInfo.objUtilityChargeInfo = utilityCharge;
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Utility charge details retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Utility charge not found.";
            }

            return objResponseInfo;
        }

        public async Task<FilterUtilityChargeResponseInfo> GetUtilityChargesAsync(FilterRequestInfo objRequestInfo)
        {
            FilterUtilityChargeResponseInfo objResponseInfo = new FilterUtilityChargeResponseInfo();

            var cmd = new SqlCommand(DBConstants.USP_RentUtilityCharge_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", objRequestInfo.objFilterInfo.PropertyId);
            cmd.Parameters.AddWithValue("@UnitId", (object?)objRequestInfo.objFilterInfo.UnitId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantAssignmentId", objRequestInfo.objFilterInfo.TenantAssignmentId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UtilityTypeId", objRequestInfo.objFilterInfo.UtilityTypeId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IsInvoiced", objRequestInfo.objFilterInfo.IsInvoiced ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", objRequestInfo.objFilterInfo.FromDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", objRequestInfo.objFilterInfo.ToDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PageNumber", objRequestInfo.objFilterInfo.PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", objRequestInfo.objFilterInfo.PageSize);

            DataTable dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                objResponseInfo.objUtilityCharges = _dbHelper.ConvertDataTable<UtilityChargeInfo>(dt);
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Utility charges retrieved successfully.";
            }
            else
            {
                objResponseInfo.objUtilityCharges = new List<UtilityChargeInfo>();
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "No utility charges found.";
            }

            return objResponseInfo;
        }
    }
}
