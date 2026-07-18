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
    /// Widget for displaying upcoming rental agreement renewals.
    /// Shows agreements that will expire soon.
    /// </summary>
    public class UpcomingRenewalsWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public UpcomingRenewalsWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "UPCOMING_RENEWALS";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetUpcomingRenewalsAsync(propertyId, userPublicId);
            return data;
        }
    }
}
