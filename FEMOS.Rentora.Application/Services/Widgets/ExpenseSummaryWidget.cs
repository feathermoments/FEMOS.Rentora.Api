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
    /// Widget for displaying expense summary information.
    /// Shows total expenses, categorized expenses, and trends.
    /// </summary>
    public class ExpenseSummaryWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public ExpenseSummaryWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "EXPENSE_SUMMARY";

        public async Task<object> GetDataAsync(Guid propertyPublicId, Guid unitPublicId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetExpenseSummaryAsync(propertyPublicId, userPublicId);
            return data;
        }
    }
}
