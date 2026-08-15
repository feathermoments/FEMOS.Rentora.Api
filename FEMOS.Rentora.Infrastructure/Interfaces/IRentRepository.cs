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
    public interface IRentRepository
    {
        Task<FilterResponseInfo> GetRentInvoicesAsync(FilterRequestInfo objRequestInfo);
        Task<RentInvoiceResponseInfo> GetRentInvoiceDetailsAsync(Guid userPublicId, long propertyId, long rentInvoiceId);
        Task<RentPaymentResponseInfo> SaveRentPaymentAsync(RentPaymentRequestInfo objRequestInfo);
        Task<FilterResponseInfo> GetRentPaymentsAsync(FilterRequestInfo objRequestInfo);
        Task<BaseResponseInfo> UpdateRentPaymentActionAsync(RentPaymentActionRequestInfo objRequestInfo);
        Task<RentPaymentResponseInfo> GetRentPaymentDetailsAsync(Guid userPublicId, long propertyId, long rentPaymentId);
        Task<FilterResponseInfo> GetTenantSecurityDepositsAsync(FilterRequestInfo objRequestInfo);
        Task<TenantSecurityDepositResponseInfo> GetTenantSecurityDepositDetailsAsync(Guid userPublicId, long tenantSecurityDepositId, long rentAgreementId, long tenantAssignmentId);
        Task<DepositTransactionListResponseInfo> GetTenantSecurityDepositTransactionsAsync(Guid userPublicId, long tenantSecurityDepositId, long rentAgreementId, long tenantAssignmentId);
    }
}
