using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface ITenantService
    {
        Task<PropertyTenantResponseInfo> GetPropertyTenantsAsync(Guid userPublicId, long propertyId);
        Task<PropertyTenantResponseInfo> GetPropertyTenantDetailsAsync(Guid userPublicId, long propertyId, long tenantId);
        Task<PropertyTenantResponseInfo> SavePropertyTenantAsync(PropertyTenantRequestInfo objRequestInfo);
        Task<PropertyTenantAssignmentResponseInfo> SavePropertyTenantAssignmentAsync(PropertyTenantAssignmentRequestInfo objRequestInfo);
        Task<PropertyTenantAssignmentResponseInfo> GetTenantAssignmentDetailsAsync(Guid userPublicId, long propertyId, long tenantId, long tenantAssignmentId);
        Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo);
    }
}
