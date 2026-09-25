using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
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
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDBHelper _dbHelper;

        public DashboardRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<DashboardWidgetInfo>> GetDashboardWidgetsByRoleAsync(long roleId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_GetWidgetsByRole);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleId", roleId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var widgets = _dbHelper.ConvertDataTable<DashboardWidgetInfo>(dt);

            return widgets;
        }

        public async Task<PropertySummaryInfo> GetPropertySummaryAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_PropertySummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<PropertySummaryInfo>(dt);
            if (data != null && data.Any()) {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<RentSummaryInfo> GetRentSummaryAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_RentSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<RentSummaryInfo>(dt);

            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<RecentPaymentInfo> GetRecentPaymentsAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_RecentPayments);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<RecentPaymentDetailInfo>(dt);
            if (data != null && data.Any())
            {
                RecentPaymentInfo objRecentPaymentInfo = new RecentPaymentInfo()
                {
                    TotalPayments = data.Count(),
                    TotalAmount = data.Where(x=>x.Status == "Verified" || x.Status == "Paid" || x.Status == "Partially Paid").Sum(x=>x.Amount),
                    RecentPayments = data
                };
                return objRecentPaymentInfo;
            }
            else
            {
                return null;
            }
        }

        public async Task<OpenRequestInfo> GetOpenRequestsAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_OpenRequests);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<OpenRequestInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<UpcomingRenewalInfo> GetUpcomingRenewalsAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_UpcomingRenewals);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<UpcomingRenewalInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<MyHomeInfo> GetMyHomeAsync(Guid propertyPublicId, Guid unitPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_MyHome);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", unitPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<MyHomeInfo>(ds.Tables[0]);
            if (data != null && data.Any())
            {
                var myHomeInfo = data.FirstOrDefault();
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    myHomeInfo.objOwnerInfo = _dbHelper.ConvertDataTable<PropertyMemberInfo>(ds.Tables[1]).FirstOrDefault();
                }
                return myHomeInfo;
            }
            else
            {
                return null;
            }
        }

        public async Task<MyAgreementInfo> GetAgreementAsync(Guid propertyPublicId, Guid unitPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_Agreement);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UnitPublicId", unitPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<MyAgreementInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<MyRequestInfo> GetMyRequestsAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_MyRequests);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<MyRequestInfo>(dt);
            if (data != null && data.Any())
            {
                MyRequestInfo objMyRequestInfo = new MyRequestInfo()
                {
                    objMyRequestSummaryInfo = data.FirstOrDefault().objMyRequestSummaryInfo,
                    objMyRequestDetails = data.FirstOrDefault().objMyRequestDetails
                };
                return objMyRequestInfo;
            }
            else
            {
                return null;
            }
        }

        public async Task<StaffSummaryInfo> GetStaffSummaryAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_StaffSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<StaffSummaryInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<ReportSummaryInfo> GetReportSummaryAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_ReportSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<ReportSummaryInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<SecurityDepositSummaryInfo> GetSecurityDepositSummaryAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_SecurityDepositSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<SecurityDepositSummaryInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<ExpenseSummaryResponseInfo> GetExpenseSummaryAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_ExpenseSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", (object?)propertyPublicId ?? DBNull.Value);

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

