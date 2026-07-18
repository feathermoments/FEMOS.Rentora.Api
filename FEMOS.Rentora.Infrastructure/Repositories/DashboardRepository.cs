using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
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

        public async Task<PropertySummaryInfo> GetPropertySummaryAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_PropertySummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

        public async Task<RentSummaryInfo> GetRentSummaryAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_RentSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

        public async Task<RecentPaymentInfo> GetRecentPaymentsAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_RecentPayments);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<RecentPaymentDetailInfo>(dt);
            if (data != null && data.Any())
            {
                RecentPaymentInfo objRecentPaymentInfo = new RecentPaymentInfo()
                {
                    TotalPayments = data.Count(),
                    RecentPayments = data
                };
                return objRecentPaymentInfo;
            }
            else
            {
                return null;
            }
        }

        public async Task<OpenRequestInfo> GetOpenRequestsAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_OpenRequests);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

        public async Task<UpcomingRenewalInfo> GetUpcomingRenewalsAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_UpcomingRenewals);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

        public async Task<MyHomeInfo> GetMyHomeAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_MyHome);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<MyHomeInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<AgreementInfo> GetAgreementAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_Agreement);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            var data = _dbHelper.ConvertDataTable<AgreementInfo>(dt);
            if (data != null && data.Any())
            {
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<MyRequestInfo> GetMyRequestsAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_MyRequests);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

        public async Task<StaffSummaryInfo> GetStaffSummaryAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_StaffSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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

        public async Task<ReportSummaryInfo> GetReportSummaryAsync(long propertyId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.USP_Dashboard_ReportSummary);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyId", propertyId);
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
    }
}

