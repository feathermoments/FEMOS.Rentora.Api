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
    /// Widget for displaying staff summary information.
    /// Shows property staff members and their status.
    /// </summary>
    public class StaffSummaryWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public StaffSummaryWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "STAFF_SUMMARY";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetStaffSummaryAsync(propertyId, userPublicId);
            return data;
        }
    }
}
