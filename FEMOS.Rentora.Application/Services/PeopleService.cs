using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
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
    internal class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IEncryptDecryptService _encryptDecryptService;

        public PeopleService(IPeopleRepository peopleRepository, IEncryptDecryptService encryptDecryptService)
        {
            _peopleRepository = peopleRepository;
            _encryptDecryptService = encryptDecryptService;
        }

        public async Task<PropertyMembersResponseInfo> GetPropertyMembersAsync(Guid propertyPublicId, Guid userPublicId)
        {
            return await _peopleRepository.GetPropertyMembersAsync(propertyPublicId, userPublicId);
        }

        public async Task<PropertyOwnersResponseInfo> GetPropertyOwnersAsync(Guid propertyPublicId, Guid userPublicId)
        {
            return await _peopleRepository.GetPropertyOwnersAsync(propertyPublicId, userPublicId);
        }

        public async Task<BaseResponseInfo> SavePropertyCoOwnerAsync(PropertyCoOwnerRequestInfo objRequestInfo)
        {
            if (!string.IsNullOrEmpty(objRequestInfo.objPropertyOwnerInfo.MobileNumber))
            {
                objRequestInfo.objPropertyOwnerInfo.MobileEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objPropertyOwnerInfo.MobileNumber);
                objRequestInfo.objPropertyOwnerInfo.MobileHash = _encryptDecryptService.ComputeHash(objRequestInfo.objPropertyOwnerInfo.MobileNumber);
            }
            if (!string.IsNullOrEmpty(objRequestInfo.objPropertyOwnerInfo.EmailAddress))
            {
                objRequestInfo.objPropertyOwnerInfo.EmailEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objPropertyOwnerInfo.EmailAddress);
                objRequestInfo.objPropertyOwnerInfo.EmailHash = _encryptDecryptService.ComputeHash(objRequestInfo.objPropertyOwnerInfo.EmailAddress);
            }
            return await _peopleRepository.SavePropertyCoOwnerAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> RemovePropertyCoOwnerAsync(Guid propertyPublicId, Guid propertyOwnerPublicId, Guid userPublicId)
        {
            return await _peopleRepository.RemovePropertyCoOwnerAsync(propertyPublicId, propertyOwnerPublicId, userPublicId);
        }

        public async Task<TenantFamilyMembersResponseInfo> GetTenantFamilyMembersAsync(Guid rentAgreementPublicId, Guid userPublicId)
        {
            return await _peopleRepository.GetTenantFamilyMembersAsync(rentAgreementPublicId, userPublicId);
        }

        public async Task<BaseResponseInfo> SaveTenantFamilyMemberAsync(TenantFamilyMemberRequestInfo objRequestInfo)
        {
            if (!string.IsNullOrEmpty(objRequestInfo.objTenantFamilyMemberInfo.MobileNumber))
            {
                objRequestInfo.objTenantFamilyMemberInfo.MobileEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objTenantFamilyMemberInfo.MobileNumber);
                objRequestInfo.objTenantFamilyMemberInfo.MobileHash = _encryptDecryptService.ComputeHash(objRequestInfo.objTenantFamilyMemberInfo.MobileNumber);
            }
            if (!string.IsNullOrEmpty(objRequestInfo.objTenantFamilyMemberInfo.EmailAddress))
            {
                objRequestInfo.objTenantFamilyMemberInfo.EmailEncrypted = _encryptDecryptService.Encrypt(objRequestInfo.objTenantFamilyMemberInfo.EmailAddress);
                objRequestInfo.objTenantFamilyMemberInfo.EmailHash = _encryptDecryptService.ComputeHash(objRequestInfo.objTenantFamilyMemberInfo.EmailAddress);
            }
            return await _peopleRepository.SaveTenantFamilyMemberAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> RemoveTenantFamilyMemberAsync(Guid rentAgreementPublicId, Guid familyMemberPublicId, Guid userPublicId)
        {
            return await _peopleRepository.RemoveTenantFamilyMemberAsync(rentAgreementPublicId, familyMemberPublicId, userPublicId);
        }
    }
}
