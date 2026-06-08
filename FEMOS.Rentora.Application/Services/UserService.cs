using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptDecryptService _encryptDecryptService;

        public UserService(IUserRepository userRepository, IEncryptDecryptService encryptDecryptService)
        {
            _userRepository = userRepository;
            _encryptDecryptService = encryptDecryptService;
        }

        /// <summary>
        /// Calls dbo.sp_GetUserProfileFull which must return 3 result sets:
        ///   #0 – profile row  (UserPublicId, Name, Email, MobileNumber, ProfilePhoto)
        ///   #1 – workspaces   (WorkspaceId, Name, Role)
        ///   #2 – stats row    (Votes, Polls)
        /// </summary>
        public async Task<UserProfileResponseInfo?> GetUserProfileAsync(Guid userPublicId)
        {
            var response = await _userRepository.GetUserProfileAsync(userPublicId);

            if (response == null)
                return new UserProfileResponseInfo();

            response.Email = _encryptDecryptService.Decrypt(response.Email);
            response.MobileNumber = _encryptDecryptService.Decrypt(response.MobileNumber);
            return response;
        }

        public async Task<BaseResponseInfo> UpdateUserProfileAsync(UserProfileInfo model)
        {
            model.EmailHash = _encryptDecryptService.ComputeHash(model.Email ?? string.Empty);
            model.MobileHash = _encryptDecryptService.ComputeHash(model.MobileNumber ?? string.Empty);

            model.EmailEncrypted = _encryptDecryptService.Encrypt(model.Email ?? string.Empty);
            model.MobileEncrypted = _encryptDecryptService.Encrypt(model.MobileNumber ?? string.Empty);

            var dbResponse = await _userRepository.UpdateUserProfileAsync(model);

            return new BaseResponseInfo { Status = dbResponse.Status, Message = dbResponse.Message };
        }

        public async Task<BaseResponseInfo> DeleteUserAccountAsync(Guid userPublicId)
        {
            var dbResponse = await _userRepository.DeleteUserAccountAsync(userPublicId);
            return new BaseResponseInfo { Status = dbResponse.Status, Message = dbResponse.Message };
        }
    }
}
