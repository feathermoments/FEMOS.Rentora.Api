using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
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
    internal class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<PropertyUnitResponseInfo> GetPropertyUnitDetailsAsync(Guid userPublicId, Guid propertyPublicId, Guid unitPublicId)
        {
            PropertyUnitResponseInfo objResponseInfo = new PropertyUnitResponseInfo();
            objResponseInfo.objPropertyUnitInfo = await _unitRepository.GetPropertyUnitDetailsAsync(userPublicId, propertyPublicId, unitPublicId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "Property unit details retrieved successfully.";
            return objResponseInfo;
        }

        public async Task<PropertyUnitResponseInfo> GetPropertyUnitsAsync(Guid userPublicId, Guid propertyPublicId)
        {
            PropertyUnitResponseInfo objResponseInfo = new PropertyUnitResponseInfo();
            objResponseInfo.objMyPropertyUnits = await _unitRepository.GetPropertyUnitsAsync(userPublicId, propertyPublicId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "Property units retrieved successfully.";
            return objResponseInfo;
        }

        public async Task<PropertyUnitResponseInfo> GetVacantUnitsAsync(Guid userPublicId, Guid propertyPublicId)
        {
            PropertyUnitResponseInfo objResponseInfo = new PropertyUnitResponseInfo();
            objResponseInfo.objMyPropertyUnits = await _unitRepository.GetVacantUnitsAsync(userPublicId, propertyPublicId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "Vacant property units retrieved successfully.";
            return objResponseInfo;
        }

        public async Task<PropertyUnitResponseInfo> SavePropertyUnitAsync(PropertyUnitRequestInfo objRequestInfo)
        {
            return await _unitRepository.SavePropertyUnitAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> DeletePropertyUnitAsync(Guid userPublicId, Guid propertyPublicId, Guid unitPublicId)
        {
            return await _unitRepository.DeletePropertyUnitAsync(userPublicId, propertyPublicId, unitPublicId);
        }
    }
}
