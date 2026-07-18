namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    using FEMOS.Rentora.Domain.Entities;

    /// <summary>
    /// Repository interface for dashboard-related operations.
    /// Handles data retrieval for dashboard configuration and widget data.
    /// </summary>
    public interface IDashboardRepository
    {
        /// <summary>
        /// Retrieves the list of dashboard widgets assigned to a specific role.
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <returns>List of dashboard widgets assigned to the role</returns>
        Task<List<DashboardWidgetInfo>> GetDashboardWidgetsByRoleAsync(long roleId);

        /// <summary>
        /// Retrieves property summary data.
        /// </summary>
        Task<PropertySummaryInfo> GetPropertySummaryAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves rent summary data.
        /// </summary>
        Task<RentSummaryInfo> GetRentSummaryAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves recent payments data.
        /// </summary>
        Task<RecentPaymentInfo> GetRecentPaymentsAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves open requests data.
        /// </summary>
        Task<OpenRequestInfo> GetOpenRequestsAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves upcoming renewals data.
        /// </summary>
        Task<UpcomingRenewalInfo> GetUpcomingRenewalsAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves my home/unit data.
        /// </summary>
        Task<MyHomeInfo> GetMyHomeAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves agreement data.
        /// </summary>
        Task<AgreementInfo> GetAgreementAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves my requests data.
        /// </summary>
        Task<MyRequestInfo> GetMyRequestsAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves staff summary data.
        /// </summary>
        Task<StaffSummaryInfo> GetStaffSummaryAsync(long propertyId, Guid userPublicId);

        /// <summary>
        /// Retrieves report summary data.
        /// </summary>
        Task<ReportSummaryInfo> GetReportSummaryAsync(long propertyId, Guid userPublicId);
    }
}
