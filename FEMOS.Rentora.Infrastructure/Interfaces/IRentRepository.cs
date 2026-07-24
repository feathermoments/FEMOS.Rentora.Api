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
        Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo);
        Task<RentAgreementInfo> GetRentAgreementAsync(Guid userPublicId, long TenantAssignmentId);
        Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, long RentAgreementId, long TenantAssignmentId);
        Task<FilterRentInvoiceResponseInfo> GetRentInvoicesAsync(FilterRentInvoiceRequestInfo objRequestInfo);
        Task<RentInvoiceResponseInfo> GetRentInvoiceDetailsAsync(Guid userPublicId, long propertyId, long rentInvoiceId);
        Task<RentPaymentResponseInfo> SaveRentPaymentAsync(RentPaymentRequestInfo objRequestInfo);
        Task<FilterRentPaymentResponseInfo> GetRentPaymentsAsync(FilterRentPaymentRequestInfo objRequestInfo);
        Task<BaseResponseInfo> UpdateRentPaymentActionAsync(RentPaymentActionRequestInfo objRequestInfo);
    }
}
