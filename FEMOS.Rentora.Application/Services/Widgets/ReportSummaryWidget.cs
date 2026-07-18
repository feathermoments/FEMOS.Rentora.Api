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
    /// Widget for displaying report summary information.
    /// Shows generated reports and analytics.
    /// </summary>
    public class ReportSummaryWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public ReportSummaryWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "REPORT_SUMMARY";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetReportSummaryAsync(propertyId, userPublicId);
            return data;
        }
    }
}
