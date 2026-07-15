using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<MyPropertyUnitInfo>> GetPropertyUnitsAsync(Guid userPublicId, long propertyId);
        Task<PropertyUnitResponseInfo> SavePropertyUnitAsync(PropertyUnitRequestInfo objRequestInfo);
        Task<PropertyUnitInfo> GetPropertyUnitDetailsAsync(Guid userPublicId, long propertyId, long unitId);
        Task<List<MyPropertyUnitInfo>> GetVacantUnitsAsync(Guid userPublicId, long propertyId);
    }
}
