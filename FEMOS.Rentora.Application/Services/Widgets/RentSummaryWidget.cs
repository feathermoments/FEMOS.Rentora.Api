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
    /// Widget for displaying rent summary information.
    /// Shows collected rent, pending rent, and agreement counts.
    /// </summary>
    public class RentSummaryWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public RentSummaryWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "RENT_SUMMARY";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetRentSummaryAsync(propertyId, userPublicId);
            return data;
        }
    }
}
