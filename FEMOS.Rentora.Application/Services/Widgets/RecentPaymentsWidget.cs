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
    /// Widget for displaying recent payment transactions.
    /// Shows recent rent payments and their status.
    /// </summary>
    public class RecentPaymentsWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public RecentPaymentsWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "RECENT_PAYMENTS";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetRecentPaymentsAsync(propertyId, userPublicId);
            return data;
        }
    }
}
