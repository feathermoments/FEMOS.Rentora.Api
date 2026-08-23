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
        /// <param name="propertyPublicId">The property ID</param>
        /// <param name="unitId">The unit ID</param>
        /// <param name="userPublicId">The authenticated user ID</param>
        /// <returns>Dashboard response containing all assigned widgets and their data</returns>
        Task<DashboardResponseInfo> GetDashboardAsync(Guid propertyPublicId, long unitId, Guid userPublicId);
    }
}
