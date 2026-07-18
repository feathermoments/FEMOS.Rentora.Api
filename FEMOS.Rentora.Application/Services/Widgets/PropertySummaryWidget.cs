using FEMOS.Rentora.Application.Interfaces.Dashboard;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services.Widgets
{
    /// <summary>
    /// Widget for displaying property summary information.
    /// Shows property details, unit counts, and status.
    /// </summary>
    public class PropertySummaryWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public PropertySummaryWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "PROPERTY_SUMMARY";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetPropertySummaryAsync(propertyId, userPublicId);
            return data;
        }
    }
}
