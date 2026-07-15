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
    internal class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IEncryptDecryptService _encryptDecryptService;
        private readonly IAccountRepository _accountRepository;
        public TenantService(ITenantRepository tenantRepository, IEncryptDecryptService encryptDecryptService, IAccountRepository accountRepository)
        {
            _tenantRepository = tenantRepository;
            _encryptDecryptService = encryptDecryptService;
            _accountRepository = accountRepository;
        }
        public async Task<PropertyTenantResponseInfo> GetPropertyTenantsAsync(Guid userPublicId, long propertyId)
        {
            PropertyTenantResponseInfo objResponseInfo = new PropertyTenantResponseInfo();
            objResponseInfo.objMyPropertyTenants = await _tenantRepository.GetPropertyTenantsAsync(userPublicId, propertyId);
            foreach (var tenant in objResponseInfo.objMyPropertyTenants)
            {
                if(!string.IsNullOrEmpty(tenant.EmailAddress))
                    tenant.EmailAddress = _encryptDecryptService.Decrypt(tenant.EmailAddress);
                if(!string.IsNullOrEmpty(tenant.MobileNumber))
                    tenant.MobileNumber = _encryptDecryptService.Decrypt(tenant.MobileNumber);
            }
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "Property tenants retrieved successfully.";
            return objResponseInfo;
        }
        public async Task<PropertyTenantResponseInfo> GetPropertyTenantDetailsAsync(Guid userPublicId, long propertyId, long tenantId)
        {
            PropertyTenantResponseInfo objResponseInfo = new PropertyTenantResponseInfo();
            objResponseInfo.objPropertyTenantInfo = await _tenantRepository.GetPropertyTenantDetailsAsync(userPublicId, propertyId, tenantId);
            if (objResponseInfo.objPropertyTenantInfo != null)
            {
                if(!string.IsNullOrEmpty(objResponseInfo.objPropertyTenantInfo.EmailAddress))
                    objResponseInfo.objPropertyTenantInfo.EmailAddress = _encryptDecryptService.Decrypt(objResponseInfo.objPropertyTenantInfo.EmailAddress);
                if(!string.IsNullOrEmpty(objResponseInfo.objPropertyTenantInfo.MobileNumber))
                    objResponseInfo.objPropertyTenantInfo.MobileNumber = _encryptDecryptService.Decrypt(objResponseInfo.objPropertyTenantInfo.MobileNumber);
            }

            if (objResponseInfo.objPropertyTenantInfo != null)
            {
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Property tenant details retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Property tenant not found.";
            }
            return objResponseInfo;
        }
        public async Task<PropertyTenantResponseInfo> SavePropertyTenantAsync(PropertyTenantRequestInfo objRequestInfo)
        {
            if(objRequestInfo.objPropertyTenantInfo?.TenantUserId == null || objRequestInfo.objPropertyTenantInfo?.TenantUserId == 0)
            {
                UserAccountInfo objUserAccountInfo =new UserAccountInfo()
                {
                    AccountStatusId = AccountStatusConstants.Active,
                    CountryId = CountryConstants.India,
                    CreatorUserId = 0,
                    EmailId = objRequestInfo.objPropertyTenantInfo.EmailAddress,
                    LanguageId = LanguageConstants.English,
                    MobileNo = objRequestInfo.objPropertyTenantInfo.MobileNumber,
                    UserPublicId = Guid.NewGuid(),
                    UserRoleId = UserRoleConstants.User
                };
                if (!string.IsNullOrEmpty(objRequestInfo.objPropertyTenantInfo.EmailAddress))
                {
                    objUserAccountInfo.EmailEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objPropertyTenantInfo.EmailAddress);
                    objUserAccountInfo.EmailHash = _encryptDecryptService.ComputeHash(objRequestInfo.objPropertyTenantInfo.EmailAddress);
                }
                if (!string.IsNullOrEmpty(objRequestInfo.objPropertyTenantInfo.MobileNumber))
                {
                    objUserAccountInfo.MobileEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objPropertyTenantInfo.MobileNumber);
                    objUserAccountInfo.MobileHash = _encryptDecryptService.ComputeHash(objRequestInfo.objPropertyTenantInfo.MobileNumber);
                }
                UserAccountResponseInfo objUserAccountResponseInfo = await _accountRepository.CreateUserAccountAsync(objUserAccountInfo);
                if (objUserAccountResponseInfo.Status == StatusConstants.Success)
                {
                    objRequestInfo.objPropertyTenantInfo.TenantUserId = objUserAccountResponseInfo.UserId;
                }
                else
                {
                    return new PropertyTenantResponseInfo
                    {
                        Status = StatusConstants.Failure,
                        Message = objUserAccountResponseInfo.Message
                    };
                }
            }
            if (!string.IsNullOrEmpty(objRequestInfo.objPropertyTenantInfo.MobileNumber))
            {
                objRequestInfo.objPropertyTenantInfo.MobileEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objPropertyTenantInfo.MobileNumber);
                objRequestInfo.objPropertyTenantInfo.MobileHash = _encryptDecryptService.ComputeHash(objRequestInfo.objPropertyTenantInfo.MobileNumber);
            }
            if(!string.IsNullOrEmpty(objRequestInfo.objPropertyTenantInfo.EmailAddress))
            {
                objRequestInfo.objPropertyTenantInfo.EmailEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objPropertyTenantInfo.EmailAddress);
                objRequestInfo.objPropertyTenantInfo.EmailHash = _encryptDecryptService.ComputeHash(objRequestInfo.objPropertyTenantInfo.EmailAddress);
            }
            return await _tenantRepository.SavePropertyTenantAsync(objRequestInfo);
        }

        public async Task<PropertyTenantAssignmentResponseInfo> SavePropertyTenantAssignmentAsync(PropertyTenantAssignmentRequestInfo objRequestInfo)
        {
            return await _tenantRepository.SavePropertyTenantAssignmentAsync(objRequestInfo);
        }

        public async Task<PropertyTenantAssignmentResponseInfo> GetTenantAssignmentDetailsAsync(Guid userPublicId, long propertyId, long tenantId, long tenantAssignmentId)
        {
            PropertyTenantAssignmentResponseInfo objResponseInfo = new PropertyTenantAssignmentResponseInfo();
            objResponseInfo.objTenantAssignmentInfo = await _tenantRepository.GetTenantAssignmentDetailsAsync(userPublicId, propertyId, tenantId, tenantAssignmentId);
            if (objResponseInfo.objTenantAssignmentInfo != null)
            {
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Tenant assignment details retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Tenant assignment not found.";
            }
            return objResponseInfo;
        }

        public async Task<TenantResponseInfo> SearchTenantAsync(Guid userPublicId, string searchText)
        {
            TenantResponseInfo objResponseInfo = new TenantResponseInfo();
            string searchTextHash = _encryptDecryptService.ComputeHash(searchText);
            List<TenantInfo> objTenants = await _tenantRepository.SearchTenantAsync(userPublicId, searchText, searchTextHash);
            foreach(TenantInfo tenantInfo in objTenants)
            {
                tenantInfo.MobileNumber = _encryptDecryptService.Decrypt(tenantInfo.MobileNumber);
                tenantInfo.EmailAddress = _encryptDecryptService.Decrypt(tenantInfo.EmailAddress);
            }
            if (objTenants != null && objTenants.Count > 0)
            {
                objResponseInfo.objTenant = objTenants[0];
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Tenants retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "No tenants found.";
            }
            return objResponseInfo;
        }
    }
}
