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
        private readonly IAuthorizationService _authorizationService;

        public AuthService(
            IJwtTokenService jwtTokenService,
            IEncryptDecryptService encryptDecryptService,
            IAuthRepository authRespository,
            IAuthorizationService authorizationService)
        {
            _jwtTokenService = jwtTokenService;
            _encryptDecryptService = encryptDecryptService;
            _authRepository = authRespository;
            _authorizationService = authorizationService;
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

            var token = "";
            var refreshToken = "";
            if (objResponseInfo.UserPublicId != Guid.Empty)
            {
                // Load authorization data (property roles and permissions)
                var authData = await _authorizationService.LoadUserAuthorizationAsync(objResponseInfo.UserPublicId);

                // Generate token with authorization data included
                if (authData.PropertyRoles.Count > 0 && authData.RolePermissions.Count > 0)
                {
                    // Convert DTOs to domain entities for token generation
                    var propertyRoles = authData.PropertyRoles.Select(pr => new PropertyRoleInfo
                    {
                        PropertyPublicId = pr.PropertyPublicId,
                        RoleId = pr.RoleId,
                        RoleCode = pr.RoleCode
                    }).ToList();

                    var rolePermissions = authData.RolePermissions.Select(rp => new RolePermissionsInfo
                    {
                        RoleId = rp.RoleId,
                        RoleCode = rp.RoleCode,
                        Permissions = rp.Permissions
                    }).ToList();

                    token = _jwtTokenService.GenerateTokenWithAuthorization(objResponseInfo.UserPublicId, propertyRoles, rolePermissions);
                }
                else
                {
                    // Fallback to legacy token generation if no authorization data
                    token = _jwtTokenService.GenerateToken(objResponseInfo.UserPublicId, objResponseInfo.Role);
                }

                refreshToken = _jwtTokenService.GenerateRefreshToken();

                // Save refresh token to database
                int refreshTokenExpiryDays = 30;
                await _authRepository.SaveRefreshTokenAsync(objResponseInfo.UserPublicId, refreshToken, refreshTokenExpiryDays);

                return new VerifyOtpResponseInfo
                {
                    Status = objResponseInfo.Status,
                    Message = objResponseInfo.Message,
                    Token = token,
                    RefreshToken = refreshToken,
                    IsNewUser = objResponseInfo.IsNewUser,
                    IsProfileComplete = objResponseInfo.IsProfileComplete
                };
            }
            else
            {
                return new VerifyOtpResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = MessageConstants.InvalidUser,
                    Token = "",
                    RefreshToken = "",
                    IsNewUser = objResponseInfo.IsNewUser,
                    IsProfileComplete = objResponseInfo.IsProfileComplete
                };
            }
        }

        public async Task<RefreshTokenResponseInfo> RefreshTokenAsync(Guid userPublicId, string refreshToken)
        {
            // Validate input
            if (string.IsNullOrEmpty(refreshToken))
            {
                return new RefreshTokenResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Refresh token is required."
                };
            }

            // Retrieve the stored refresh token for this user
            var refreshTokenInfo = await _authRepository.GetRefreshTokenAsync(userPublicId);

            // Validate token exists
            if (string.IsNullOrEmpty(refreshTokenInfo.Token))
            {
                return new RefreshTokenResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Invalid refresh token. No token found for user."
                };
            }

            // Validate token matches the one provided by the user
            if (refreshTokenInfo.Token != refreshToken)
            {
                return new RefreshTokenResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Invalid refresh token. Token mismatch."
                };
            }

            // Validate token is not revoked
            if (refreshTokenInfo.IsRevoked)
            {
                return new RefreshTokenResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Refresh token has been revoked."
                };
            }

            // Validate token has not expired
            if (refreshTokenInfo.ExpiryDate < DateTime.UtcNow)
            {
                return new RefreshTokenResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Refresh token has expired."
                };
            }

            // All validations passed - generate new token with fresh authorization data
            // Load current authorization data to ensure changes are reflected
            var authData = await _authorizationService.LoadUserAuthorizationAsync(userPublicId);

            string newToken;

            if (authData.PropertyRoles.Count > 0 && authData.RolePermissions.Count > 0)
            {
                // Convert DTOs to domain entities for token generation
                var propertyRoles = authData.PropertyRoles.Select(pr => new PropertyRoleInfo
                {
                    PropertyPublicId = pr.PropertyPublicId,
                    RoleId = pr.RoleId,
                    RoleCode = pr.RoleCode
                }).ToList();

                var rolePermissions = authData.RolePermissions.Select(rp => new RolePermissionsInfo
                {
                    RoleId = rp.RoleId,
                    RoleCode = rp.RoleCode,
                    Permissions = rp.Permissions
                }).ToList();

                newToken = _jwtTokenService.GenerateTokenWithAuthorization(userPublicId, propertyRoles, rolePermissions);
            }
            else
            {
                // Fallback to legacy token generation if no authorization data
                newToken = _jwtTokenService.GenerateToken(userPublicId, "User");
            }

            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            // Save new refresh token (this will revoke the old one)
            await _authRepository.SaveRefreshTokenAsync(userPublicId, newRefreshToken, 30);

            return new RefreshTokenResponseInfo
            {
                Status = StatusConstants.Success,
                Message = ApiConstants.Successfull,
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<BaseResponseInfo> LogoutAsync(Guid userPublicId)
        {
            var result = await _authRepository.RevokeRefreshTokenAsync(userPublicId);

            if (result)
            {
                return new BaseResponseInfo
                {
                    Status = StatusConstants.Success,
                    Message = "Logged out successfully."
                };
            }
            else
            {
                return new BaseResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Failed to logout."
                };
            }
        }

        public async Task<BaseResponseInfo> LogoutAllAsync(Guid userPublicId)
        {
            var result = await _authRepository.RevokeAllRefreshTokensAsync(userPublicId);

            if (result)
            {
                return new BaseResponseInfo
                {
                    Status = StatusConstants.Success,
                    Message = "Logged out from all devices successfully."
                };
            }
            else
            {
                return new BaseResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Failed to logout from all devices."
                };
            }
        }
    }
}
