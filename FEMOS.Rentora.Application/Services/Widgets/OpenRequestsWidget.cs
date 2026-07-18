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
    /// Widget for displaying open maintenance and tenant requests.
    /// Shows pending requests that require attention.
    /// </summary>
    public class OpenRequestsWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public OpenRequestsWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "OPEN_REQUESTS";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetOpenRequestsAsync(propertyId, userPublicId);
            return data;
        }
    }
}
