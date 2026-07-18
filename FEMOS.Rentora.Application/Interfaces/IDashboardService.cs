namespace FEMOS.Rentora.Application.Interfaces
{
    using FEMOS.Rentora.Domain.Responses;

    /// <summary>
    /// Service interface for dashboard operations.
    /// Orchestrates widget retrieval and aggregation.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Retrieves the complete dashboard for a user with all assigned widgets.
        /// </summary>
        /// <param name="propertyId">The property ID</param>
        /// <param name="userId">The authenticated user ID</param>
        /// <param name="roleId">The user's role ID</param>
        /// <returns>Dashboard response containing all assigned widgets and their data</returns>
        Task<DashboardResponseInfo> GetDashboardAsync(long propertyId, Guid userPublicId);
    }
}
