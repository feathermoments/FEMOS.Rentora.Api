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
    public interface IPeopleRepository
    {
        // Property Members
        Task<PropertyMembersResponseInfo> GetPropertyMembersAsync(Guid propertyPublicId, Guid userPublicId);
        Task<PropertyOwnersResponseInfo> GetPropertyOwnersAsync(Guid propertyPublicId, Guid userPublicId);
        Task<BaseResponseInfo> SavePropertyCoOwnerAsync(PropertyCoOwnerRequestInfo objRequestInfo);
        Task<BaseResponseInfo> RemovePropertyCoOwnerAsync(Guid propertyPublicId, Guid propertyOwnerPublicId, Guid userPublicId);

        // Tenant Family Members
        Task<TenantFamilyMembersResponseInfo> GetTenantFamilyMembersAsync(Guid rentAgreementPublicId, Guid userPublicId);
        Task<BaseResponseInfo> SaveTenantFamilyMemberAsync(TenantFamilyMemberRequestInfo objRequestInfo);
        Task<BaseResponseInfo> RemoveTenantFamilyMemberAsync(Guid rentAgreementPublicId, Guid familyMemberPublicId, Guid userPublicId);
    }
}
