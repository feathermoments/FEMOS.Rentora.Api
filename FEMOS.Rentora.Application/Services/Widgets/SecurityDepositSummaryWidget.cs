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
    /// Widget for displaying security deposit summary information.
    /// Shows deposit collection, pending approvals, and refund status.
    /// </summary>
    public class SecurityDepositSummaryWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public SecurityDepositSummaryWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "SECURITY_DEPOSIT_SUMMARY";

        public async Task<object> GetDataAsync(long propertyId, long unitId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetSecurityDepositSummaryAsync(propertyId, userPublicId);
            return data;
        }
    }
}
