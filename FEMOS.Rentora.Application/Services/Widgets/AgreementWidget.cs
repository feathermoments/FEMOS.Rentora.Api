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
    /// Widget for displaying rental agreements information.
    /// Shows active and past agreements with details.
    /// </summary>
    public class AgreementWidget : IDashboardWidget
    {
        private readonly IDashboardRepository _dashboardRepository;

        public AgreementWidget(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public string WidgetCode => "MY_AGREEMENT";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            var data = await _dashboardRepository.GetAgreementAsync(propertyId, userPublicId);
            return data;
        }
    }
}
