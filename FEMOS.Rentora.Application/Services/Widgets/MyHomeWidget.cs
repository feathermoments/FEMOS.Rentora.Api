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
    /// Widget for displaying tenant's home/unit information.
    /// Shows the unit details where the tenant resides.
    /// </summary>
    public class MyHomeWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public MyHomeWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "MY_HOME";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetMyHomeAsync(propertyId, userPublicId);
            return data;
        }
    }
}
