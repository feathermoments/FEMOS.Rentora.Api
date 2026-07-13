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
    public interface ITenantRepository
    {
        Task<List<MyPropertyTenantInfo>> GetPropertyTenantsAsync(Guid userPublicId, long propertyId);
        Task<PropertyTenantInfo> GetPropertyTenantDetailsAsync(Guid userPublicId, long propertyId, long tenantId);
        Task<PropertyTenantResponseInfo> SavePropertyTenantAsync(PropertyTenantRequestInfo objRequestInfo);
        Task<PropertyTenantAssignmentResponseInfo> SavePropertyTenantAssignmentAsync(PropertyTenantAssignmentRequestInfo objRequestInfo);
        Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo);
    }
}
