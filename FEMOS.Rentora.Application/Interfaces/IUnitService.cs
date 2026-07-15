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
        Task<PropertyUnitResponseInfo> GetPropertyUnitsAsync(Guid userPublicId, long propertyId);
        Task<PropertyUnitResponseInfo> SavePropertyUnitAsync(PropertyUnitRequestInfo objRequestInfo);
        Task<PropertyUnitResponseInfo> GetPropertyUnitDetailsAsync(Guid userPublicId, long propertyId, long unitId);
        Task<PropertyUnitResponseInfo> GetVacantUnitsAsync(Guid userPublicId, long propertyId);
    }
}
