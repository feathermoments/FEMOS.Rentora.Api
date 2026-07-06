using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<MyPropertyResponseInfo> GetMyPropertiesAsync(Guid userPublicId)
        {
            MyPropertyResponseInfo objResponseInfo = new MyPropertyResponseInfo();
            objResponseInfo.objProperties = await _propertyRepository.GetMyPropertiesAsync(userPublicId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "Properties retrieved successfully.";
            return objResponseInfo;
        }

        public async Task<UserPropertyResponseInfo> GetPropertyDetailsAsync(Guid userPublicId, long propertyId)
        {
            UserPropertyResponseInfo objResponseInfo = new UserPropertyResponseInfo();
            objResponseInfo.objUserPropertyInfo = await _propertyRepository.GetPropertyDetailsAsync(userPublicId, propertyId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "Property details retrieved successfully.";
            return objResponseInfo;
        }

        public async Task<UserPropertyResponseInfo> SavePropertyAsync(UserPropertyRequestInfo objRequestInfo)
        {
            return await _propertyRepository.SavePropertyAsync(objRequestInfo);
        }
    }
}
