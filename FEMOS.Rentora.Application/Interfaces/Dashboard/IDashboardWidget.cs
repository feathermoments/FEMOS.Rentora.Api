namespace FEMOS.Rentora.Application.Interfaces.Dashboard
{
    /// <summary>
    /// Interface for dashboard widgets following the Strategy Pattern.
    /// Each widget is responsible for fetching and returning its own data.
    /// </summary>
    public interface IDashboardWidget
    {
        /// <summary>
        /// Gets the unique code/identifier for this widget.
        /// </summary>
        string WidgetCode { get; }

        /// <summary>
        /// Asynchronously retrieves the widget data.
        /// </summary>
        /// <param name="propertyId">The property ID for which to fetch data</param>
        /// <param name="userPublicId">The public ID of the user requesting the data</param>
        /// <returns>Widget-specific data as an object</returns>
        Task<object> GetDataAsync(long propertyId, Guid userPublicId);
    }
}
