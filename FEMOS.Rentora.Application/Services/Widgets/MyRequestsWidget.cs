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
    /// Widget for displaying user's own requests.
    /// Shows requests made by the tenant or property owner.
    /// </summary>
    public class MyRequestsWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public MyRequestsWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "MY_REQUESTS";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetMyRequestsAsync(propertyId, userPublicId);
            return data;
        }
    }
}
