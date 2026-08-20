using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IEncryptDecryptService _encryptDecryptService;
        private readonly IAuthRepository _authRepository;
        private readonly IPermissionService _permissionService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(
            IJwtTokenService jwtTokenService, 
            IEncryptDecryptService encryptDecryptService, 
            IAuthRepository authRespository,
            IPermissionService permissionService,
            IRefreshTokenService refreshTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _encryptDecryptService = encryptDecryptService;
            _authRepository = authRespository;
            _permissionService = permissionService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<SendOtpResponseInfo> SendOtpAsync(SendOtpInfo model)
        {
            model.ContactHash = _encryptDecryptService.ComputeHash(model.Identifier); // hash the identifier for privacy
            model.ContactEncrypted = _encryptDecryptService.Encrypt(model.Identifier); // encrypt the identifier for privacy
            HelperUtility helperUtility = new HelperUtility();
            string Otp = helperUtility.GenerateOtp(); // generate a 6-digit OTP
            model.OtpHash = _encryptDecryptService.ComputeHash(Otp); // hash the OTP for privacy
            model.OtpEncrypted = _encryptDecryptService.Encrypt(Otp); // encrypt the OTP for privacy

            SendOtpResponseInfo otpResponseInfo = await _authRepository.SendOtpAsync(model, Otp);

            return otpResponseInfo;
        }

        public async Task<VerifyOtpResponseInfo> VerifyOtpAsync(VerifyOtpInfo model)
        {
            model.ContactHash = _encryptDecryptService.ComputeHash(model.Identifier); // hash the identifier for privacy
            model.ContactEncrypted = _encryptDecryptService.Encrypt(model.Identifier);
            model.OtpHash = _encryptDecryptService.ComputeHash(model.Otp); // hash the OTP for privacy

            DBAuthResponseInfo objResponseInfo = await _authRepository.VerifyOtpAsync(model);

            if (objResponseInfo.UserPublicId != Guid.Empty)
            {
                // For Rentora: User authenticates with global role USER (2)
                // Do NOT load permissions - permissions are property-specific
                // Permissions will be loaded after user selects a property
                var permissions = new List<string>(); // Empty at login

                // Get accessible properties for property selection
                var accessibleProperties = await _permissionService.GetAccessiblePropertiesAsync(objResponseInfo.UserPublicId);

                // Generate access token with NO permissions (global role)
                var accessToken = _jwtTokenService.GenerateToken(objResponseInfo.UserPublicId, objResponseInfo.Role, permissions);

                // Generate refresh token
                var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(objResponseInfo.UserId, objResponseInfo.UserPublicId);

                return new VerifyOtpResponseInfo
                {
                    Status = objResponseInfo.Status,
                    Message = objResponseInfo.Message,
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    Permissions = permissions,  // Empty at login
                    RoleId = objResponseInfo.RoleId,  // Global role = 2
                    IsNewUser = objResponseInfo.IsNewUser,
                    IsProfileComplete = objResponseInfo.IsProfileComplete,
                    AccessibleProperties = accessibleProperties  // For property selection
                };
            }
            else
            {
                return new VerifyOtpResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = MessageConstants.InvalidUser,
                    Token = "",
                    IsNewUser = objResponseInfo.IsNewUser,
                    IsProfileComplete = objResponseInfo.IsProfileComplete
                };
            }
        }
    }
}
