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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IDBHelper _dbHelper;

        public ExpenseRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ExpenseResponseInfo> AddExpenseAsync(ExpenseCreateRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyExpense_Add);
            cmd.CommandType = CommandType.StoredProcedure;
            var ExpensePublicIdParam = new SqlParameter("@ExpensePublicId", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Output,
                Value = DBNull.Value
            };
            cmd.Parameters.Add(ExpensePublicIdParam);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", (object?)objRequestInfo.UnitPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantPublicId", (object?)objRequestInfo.TenantPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaintenanceRequestPublicId", (object?)objRequestInfo.MaintenanceRequestPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ExpenseCategoryId", objRequestInfo.ExpenseCategoryId);
            cmd.Parameters.AddWithValue("@ExpenseTitle", objRequestInfo.ExpenseTitle);
            cmd.Parameters.AddWithValue("@Amount", objRequestInfo.Amount);
            cmd.Parameters.AddWithValue("@ExpenseDate", objRequestInfo.ExpenseDate);
            cmd.Parameters.AddWithValue("@VendorName", (object?)objRequestInfo.VendorName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceUrl", (object?)objRequestInfo.InvoiceUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)objRequestInfo.Notes ?? DBNull.Value);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            Guid? ExpensePublicId = (Guid?)(ExpensePublicIdParam.Value != DBNull.Value
                ? ExpensePublicIdParam.Value
                : null);
            if (ExpensePublicId.HasValue)
            {
                return new ExpenseResponseInfo()
                {
                    ExpensePublicId = ExpensePublicId,
                    Status = dbResponse.Status,
                    Message = dbResponse.Message
                };
            }
            else
            {
                return new ExpenseResponseInfo()
                {
                    Status = dbResponse.Status,
                    Message = dbResponse.Message
                };
            }
        }

        public async Task<ExpenseResponseInfo> GetExpenseAsync(Guid userPublicId, Guid expensePublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyExpense_Get);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@ExpensePublicId", expensePublicId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var expenses = _dbHelper.ConvertDataTable<ExpenseInfo>(ds.Tables[0]);
                if (expenses != null && expenses.Count > 0)
                {
                    var expense = expenses[0];
                    return new ExpenseResponseInfo()
                    {
                        objExpenseInfo = new ExpenseDetailInfo()
                        {
                            ExpensePublicId = expense.ExpensePublicId,
                            PropertyPublicId = expense.PropertyPublicId,
                            PropertyName = expense.PropertyName,
                            UnitPublicId = expense.UnitPublicId,
                            UnitName = expense.UnitName,
                            TenantPublicId = expense.TenantPublicId,
                            TenantName = expense.TenantName,
                            ExpenseCategoryId = expense.ExpenseCategoryId,
                            ExpenseCategoryName = expense.ExpenseCategoryName,
                            MaintenanceRequestPublicId = expense.MaintenanceRequestPublicId,
                            ExpenseTitle = expense.ExpenseTitle,
                            Amount = expense.Amount,
                            ExpenseDate = expense.ExpenseDate,
                            VendorName = expense.VendorName,
                            InvoiceUrl = expense.InvoiceUrl,
                            Notes = expense.Notes,
                            CreatedOn = expense.CreatedOn
                        },
                        Status = StatusConstants.Success,
                        Message = "Expense details retrieved successfully."
                    };
                }
            }

            return new ExpenseResponseInfo()
            {
                Status = "Failed",
                Message = "Expense not found."
            };
        }

        public async Task<ExpenseListResponseInfo> GetExpensesAsync(FilterRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyExpense_List);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", (object?)objRequestInfo.objFilterInfo.PropertyPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UnitPublicId", (object?)objRequestInfo.objFilterInfo.UnitPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantPublicId", (object?)objRequestInfo.objFilterInfo.TenantPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ExpenseCategoryId", (object?)objRequestInfo.objFilterInfo.ExpenseCategoryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", (object?)objRequestInfo.objFilterInfo.FromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object?)objRequestInfo.objFilterInfo.ToDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PageNumber", objRequestInfo.objFilterInfo.PageNumber);
            cmd.Parameters.AddWithValue("@PageSize", objRequestInfo.objFilterInfo.PageSize);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var expenses = _dbHelper.ConvertDataTable<ExpenseListItemInfo>(dt);

            return new ExpenseListResponseInfo()
            {
                objExpenses = expenses ?? new List<ExpenseListItemInfo>(),
                Status = StatusConstants.Success,
                Message = "Expenses retrieved successfully."
            };
        }

        public async Task<BaseResponseInfo> UpdateExpenseAsync(ExpenseUpdateRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyExpense_Update);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
            cmd.Parameters.AddWithValue("@ExpensePublicId", objRequestInfo.ExpensePublicId);
            cmd.Parameters.AddWithValue("@ExpenseCategoryId", objRequestInfo.ExpenseCategoryId);
            cmd.Parameters.AddWithValue("@ExpenseTitle", objRequestInfo.ExpenseTitle);
            cmd.Parameters.AddWithValue("@Amount", objRequestInfo.Amount);
            cmd.Parameters.AddWithValue("@ExpenseDate", objRequestInfo.ExpenseDate);
            cmd.Parameters.AddWithValue("@VendorName", (object?)objRequestInfo.VendorName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceUrl", (object?)objRequestInfo.InvoiceUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)objRequestInfo.Notes ?? DBNull.Value);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            if (!string.IsNullOrEmpty(result))
            {
                return new BaseResponseInfo()
                {
                    Status = dbResponse.Status,
                    Message = dbResponse.Message
                };
            }

            return new BaseResponseInfo()
            {
                Status = "Failed",
                Message = "Failed to update expense."
            };
        }

        public async Task<BaseResponseInfo> DeleteExpenseAsync(Guid userPublicId, Guid expensePublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_PropertyExpense_Delete);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@ExpensePublicId", expensePublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            if (!string.IsNullOrEmpty(result))
            {
                return new BaseResponseInfo()
                {
                    Status = dbResponse.Status,
                    Message = dbResponse.Message
                };
            }

            return new BaseResponseInfo()
            {
                Status = "Failed",
                Message = "Failed to delete expense."
            };
        }

        public async Task<ExpenseSummaryResponseInfo> GetExpenseSummaryAsync(Guid userPublicId, Guid? propertyPublicId, Guid? unitPublicId, DateTime? fromDate, DateTime? toDate)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_ExpenseSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", (object?)propertyPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UnitPublicId", (object?)unitPublicId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", (object?)fromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object?)toDate ?? DBNull.Value);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

            var response = new ExpenseSummaryResponseInfo()
            {
                Status = StatusConstants.Success,
                Message = "Expense summary retrieved successfully."
            };

            // Result Set 1: Overall Summary
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.TotalExpense = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalExpense"] ?? 0);
                response.ExpenseCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ExpenseCount"] ?? 0);
            }

            // Result Set 2: Category Summary
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                var categories = _dbHelper.ConvertDataTable<ExpenseCategorySummaryInfo>(ds.Tables[1]);
                if (categories != null)
                {
                    response.Categories = categories;
                }
            }

            // Result Set 3: Monthly Trend
            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                var monthlyTrend = _dbHelper.ConvertDataTable<ExpenseMonthlyTrendInfo>(ds.Tables[2]);
                if (monthlyTrend != null)
                {
                    response.MonthlyTrend = monthlyTrend;
                }
            }

            return response;
        }
    }
}
