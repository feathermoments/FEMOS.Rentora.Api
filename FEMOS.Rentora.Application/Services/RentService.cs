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

        public async Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, long RentAgreementId, long TenantAssignmentId)
        {
            return await _rentRepository.DeleteRentAgreementAsync(userPublicId, RentAgreementId, TenantAssignmentId);
        }

        public async Task<RentAgreementResponseInfo> GetRentAgreementAsync(Guid userPublicId, long TenantAssignmentId)
        {
            RentAgreementResponseInfo objResponseInfo = new RentAgreementResponseInfo();
            objResponseInfo.objRentAgreementInfo = await _rentRepository.GetRentAgreementAsync(userPublicId, TenantAssignmentId);
            if (objResponseInfo.objRentAgreementInfo != null)
            {
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Rent agreement details retrieved successfully.";
            }
            else
            {
                objResponseInfo.objRentAgreementInfo = new RentAgreementInfo()
                {
                    RentAgreementId = 0,
                    TenantAssignmentId = TenantAssignmentId,
                    AgreementStatusId = 4,
                    AgreementStatus = "Draft (Pending)"
                };
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Rent agreement not found. Create a new draft.";
            }
            return objResponseInfo;
        }

        public async Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo)
        {
            return await _rentRepository.SaveRentAgreementAsync(objRequestInfo);
        }

        public async Task<FilterRentInvoiceResponseInfo> GetRentInvoicesAsync(FilterRentInvoiceRequestInfo objRequestInfo)
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

        public async Task<RentPaymentResponseInfo> SaveRentPaymentAsync(RentPaymentRequestInfo objRequestInfo)
        {
            return await _rentRepository.SaveRentPaymentAsync(objRequestInfo);
        }
    }
}
