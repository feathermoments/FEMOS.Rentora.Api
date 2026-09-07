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

        public async Task<BaseResponseInfo> AddPropertyCoOwnerAsync(PropertyCoOwnerRequestInfo objRequestInfo)
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
            return await _peopleRepository.AddPropertyCoOwnerAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> UpdatePropertyCoOwnerAsync(UpdatePropertyCoOwnerRequestInfo objRequestInfo)
        {
            return await _peopleRepository.UpdatePropertyCoOwnerAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> RemovePropertyCoOwnerAsync(Guid propertyPublicId, Guid propertyOwnerPublicId, Guid userPublicId)
        {
            return await _peopleRepository.RemovePropertyCoOwnerAsync(propertyPublicId, propertyOwnerPublicId, userPublicId);
        }

        public async Task<TenantFamilyMembersResponseInfo> GetTenantFamilyMembersAsync(Guid rentAgreementPublicId, Guid userPublicId)
        {
            return await _peopleRepository.GetTenantFamilyMembersAsync(rentAgreementPublicId, userPublicId);
        }

        public async Task<BaseResponseInfo> AddTenantFamilyMemberAsync(TenantFamilyMemberRequestInfo objRequestInfo)
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
            return await _peopleRepository.AddTenantFamilyMemberAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> UpdateTenantFamilyMemberAsync(UpdateTenantFamilyMemberRequestInfo objRequestInfo)
        {
            return await _peopleRepository.UpdateTenantFamilyMemberAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> RemoveTenantFamilyMemberAsync(Guid rentAgreementPublicId, Guid familyMemberPublicId, Guid userPublicId)
        {
            return await _peopleRepository.RemoveTenantFamilyMemberAsync(rentAgreementPublicId, familyMemberPublicId, userPublicId);
        }

        public async Task<SearchUserResponseInfo> SearchUser(Guid userPublicId, string searchText)
        {
            SearchUserResponseInfo objResponseInfo = new SearchUserResponseInfo();
            string searchTextHash = _encryptDecryptService.ComputeHash(searchText);
            List<MemberUserInfo> objMemberUsers = await _peopleRepository.SearchUser(userPublicId, searchText, searchTextHash);
            foreach (MemberUserInfo memberUserInfo   in objMemberUsers)
            {
                memberUserInfo.MobileNumber = _encryptDecryptService.Decrypt(memberUserInfo.MobileNumber);
                memberUserInfo.EmailAddress = _encryptDecryptService.Decrypt(memberUserInfo.EmailAddress);
            }
            if (objMemberUsers != null && objMemberUsers.Count > 0)
            {
                objResponseInfo.objMemberUsers = objMemberUsers;
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Members retrieved successfully.";
            }
            else
            {
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "No members found.";
            }
            return objResponseInfo;
        }

        public async Task<SearchUserResponseInfo> SearchUserForPropertyRoleAsync(string searchText, Guid userPublicId, Guid propertyPublicId, Guid rentAgreementPublicId, string memberRoleCode)
        {
			string searchTextHash = _encryptDecryptService.ComputeHash(searchText);
			var response = await _peopleRepository.SearchUserForPropertyRoleAsync(searchText, userPublicId, propertyPublicId, rentAgreementPublicId, memberRoleCode, searchTextHash);

            if (response?.objMemberInfo != null)
            {
                if (!string.IsNullOrEmpty(response.objMemberInfo.MobileNumber))
                {
                    response.objMemberInfo.MobileNumber = _encryptDecryptService.Decrypt(response.objMemberInfo.MobileNumber);
                }
                if (!string.IsNullOrEmpty(response.objMemberInfo.EmailAddress))
                {
                    response.objMemberInfo.EmailAddress = _encryptDecryptService.Decrypt(response.objMemberInfo.EmailAddress);
                }
            }

            return response;
        }
    }
}
