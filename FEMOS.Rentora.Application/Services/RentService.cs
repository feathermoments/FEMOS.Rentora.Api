using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IEncryptDecryptService _encryptDecryptService;
        public RentService(IRentRepository rentRepository, IEncryptDecryptService encryptDecryptService)
        {
            _rentRepository = rentRepository;
            _encryptDecryptService = encryptDecryptService;
        }

        //Invoice
        public async Task<FilterRentInvoiceResponseInfo> GetRentInvoicesAsync(FilterRequestInfo objRequestInfo)
        {
            return await _rentRepository.GetRentInvoicesAsync(objRequestInfo);
        }

        public async Task<RentInvoiceResponseInfo> GetRentInvoiceDetailsAsync(Guid userPublicId, long propertyId, long rentInvoiceId)
        {
            RentInvoiceResponseInfo objResponseInfo = await _rentRepository.GetRentInvoiceDetailsAsync(userPublicId, propertyId, rentInvoiceId);
            if(objResponseInfo.objPropertyOwnerInfo != null)
            {
                objResponseInfo.objPropertyOwnerInfo.MobileNumber = _encryptDecryptService.Decrypt(objResponseInfo.objPropertyOwnerInfo.MobileNumber);
                objResponseInfo.objPropertyOwnerInfo.EmailAddress = _encryptDecryptService.Decrypt(objResponseInfo.objPropertyOwnerInfo.EmailAddress);
            }
            if (objResponseInfo.objPropertyTenantInfo != null)
            {
                objResponseInfo.objPropertyTenantInfo.MobileNumber = _encryptDecryptService.Decrypt(objResponseInfo.objPropertyTenantInfo.MobileNumber);
                objResponseInfo.objPropertyTenantInfo.EmailAddress = _encryptDecryptService.Decrypt(objResponseInfo.objPropertyTenantInfo.EmailAddress);
            }
            return objResponseInfo;
        }

        //Payment
        public async Task<RentPaymentResponseInfo> SaveRentPaymentAsync(RentPaymentRequestInfo objRequestInfo)
        {
            return await _rentRepository.SaveRentPaymentAsync(objRequestInfo);
        }

        public async Task<FilterRentPaymentResponseInfo> GetRentPaymentsAsync(FilterRequestInfo objRequestInfo)
        {
            return await _rentRepository.GetRentPaymentsAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> UpdateRentPaymentActionAsync(RentPaymentActionRequestInfo objRequestInfo)
        {
            return await _rentRepository.UpdateRentPaymentActionAsync(objRequestInfo);
        }

        public async Task<RentPaymentResponseInfo> GetRentPaymentDetailsAsync(Guid userPublicId, long propertyId, long rentPaymentId)
        {
            RentPaymentResponseInfo objResponseInfo = await _rentRepository.GetRentPaymentDetailsAsync(userPublicId, propertyId, rentPaymentId);
            if (objResponseInfo.objRentPaymentInfo != null)
            {
                objResponseInfo.objRentPaymentInfo.MobileNumber = _encryptDecryptService.Decrypt(objResponseInfo.objRentPaymentInfo.MobileNumber);
                objResponseInfo.objRentPaymentInfo.EmailAddress = _encryptDecryptService.Decrypt(objResponseInfo.objRentPaymentInfo.EmailAddress);
            }
            return objResponseInfo;
        }

        public async Task<FilterTenantSecurityDepositResponseInfo> GetTenantSecurityDepositsAsync(FilterRequestInfo objRequestInfo)
        {
            return await _rentRepository.GetTenantSecurityDepositsAsync(objRequestInfo);
        }

        public async Task<TenantSecurityDepositResponseInfo> GetTenantSecurityDepositDetailsAsync(Guid userPublicId, long tenantSecurityDepositId, long rentAgreementId, long tenantAssignmentId)
        {
            return await _rentRepository.GetTenantSecurityDepositDetailsAsync(userPublicId, tenantSecurityDepositId, rentAgreementId, tenantAssignmentId);
        }

        public async Task<DepositTransactionListResponseInfo> GetTenantSecurityDepositTransactionsAsync(Guid userPublicId, long tenantSecurityDepositId, long rentAgreementId, long tenantAssignmentId)
        {
            return await _rentRepository.GetTenantSecurityDepositTransactionsAsync(userPublicId, tenantSecurityDepositId, rentAgreementId, tenantAssignmentId);
        }
    }
}
