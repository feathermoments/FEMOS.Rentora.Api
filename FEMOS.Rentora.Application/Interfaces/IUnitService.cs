using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IUnitService
    {
        Task<PropertyUnitResponseInfo> GetPropertyUnitsAsync(Guid userPublicId, Guid propertyPublicId);
        Task<PropertyUnitResponseInfo> SavePropertyUnitAsync(PropertyUnitRequestInfo objRequestInfo);
        Task<PropertyUnitResponseInfo> GetPropertyUnitDetailsAsync(Guid userPublicId, Guid propertyPublicId, Guid unitPublicId);
        Task<PropertyUnitResponseInfo> GetVacantUnitsAsync(Guid userPublicId, Guid propertyPublicId);
        Task<BaseResponseInfo> DeletePropertyUnitAsync(Guid userPublicId, Guid propertyPublicId, Guid unitPublicId);
    }
}
