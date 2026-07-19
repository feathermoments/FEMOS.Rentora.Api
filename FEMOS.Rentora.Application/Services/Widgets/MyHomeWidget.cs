using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Application.Interfaces.Dashboard;
using FEMOS.Rentora.Domain.Entities;
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
        private readonly IEncryptDecryptService _encryptDecryptService;

        public MyHomeWidget(IDashboardRepository dashboardRepository, IEncryptDecryptService encryptDecryptService)
        {
            _dashboardRepository = dashboardRepository;
            _encryptDecryptService = encryptDecryptService;
        }

        public string WidgetCode => "MY_HOME";

        public async Task<object> GetDataAsync(long propertyId, Guid userPublicId)
        {
            MyHomeInfo objMyHomeInfo = await _dashboardRepository.GetMyHomeAsync(propertyId, userPublicId);
            if (objMyHomeInfo.objOwnerInfo != null)
            {
                objMyHomeInfo.objOwnerInfo.MobileNumber = _encryptDecryptService.Decrypt(objMyHomeInfo.objOwnerInfo.MobileNumber);
                objMyHomeInfo.objOwnerInfo.EmailAddress = _encryptDecryptService.Decrypt(objMyHomeInfo.objOwnerInfo.EmailAddress);
            }
            return objMyHomeInfo;
        }
    }
}
