using FEMOS.Rentora.Domain.Entities;
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
    public interface IPropertyRepository
    {
        Task<List<MyPropertyInfo>> GetMyPropertiesAsync(Guid userPublicId);
        Task<UserPropertyResponseInfo> SavePropertyAsync(UserPropertyRequestInfo objRequestInfo);
        Task<UserPropertyInfo> GetPropertyDetailsAsync(Guid userPublicId, long propertyId);
    }
}
