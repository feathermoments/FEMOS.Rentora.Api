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
        Task<PropertyTenantResponseInfo> GetPropertyTenantsAsync(Guid userPublicId, Guid PropertyPublicId);
        Task<PropertyTenantResponseInfo> GetPropertyTenantDetailsAsync(Guid userPublicId, Guid PropertyPublicId, long tenantId);
        Task<PropertyTenantResponseInfo> SavePropertyTenantAsync(PropertyTenantRequestInfo objRequestInfo);
        Task<PropertyTenantAssignmentResponseInfo> SavePropertyTenantAssignmentAsync(PropertyTenantAssignmentRequestInfo objRequestInfo);
        Task<PropertyTenantAssignmentResponseInfo> GetTenantAssignmentDetailsAsync(Guid userPublicId, Guid propertyPublicId, long tenantId, Guid tenantAssignmentPublicId);
        Task<TenantResponseInfo> SearchTenantAsync(Guid userPublicId, string searchText);
        Task<BaseResponseInfo> DeletePropertyTenantAsync(Guid userPublicId, Guid propertyPublicId, long tenantId);
        Task<BaseResponseInfo> DeleteTenantAssignmentAsync(Guid userPublicId, Guid propertyPublicId, Guid tenantAssignmentPublicId);
    }
}
