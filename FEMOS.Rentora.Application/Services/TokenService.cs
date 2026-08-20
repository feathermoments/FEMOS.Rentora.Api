using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class TokenService : ITokenService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IPermissionService _permissionService;
        private readonly IAuthRepository _authRepository;

        public TokenService(
            IJwtTokenService jwtTokenService,
            IRefreshTokenService refreshTokenService,
            IPermissionService permissionService,
            IAuthRepository authRepository)
        {
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
            _permissionService = permissionService;
            _authRepository = authRepository;
        }

        public async Task<VerifyOtpResponseInfo> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            try
            {
                // Parse the access token to extract user info
                var jwtHandler = new JwtSecurityTokenHandler();

                if (!jwtHandler.CanReadToken(accessToken))
                {
                    return new VerifyOtpResponseInfo
                    {
                        Status = StatusConstants.Failure,
                        Message = "Invalid access token format."
                    };
                }

                var jwtToken = jwtHandler.ReadJwtToken(accessToken);

                // Extract user public ID from token
                var userPublicIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userPublicId");
                if (userPublicIdClaim == null || !Guid.TryParse(userPublicIdClaim.Value, out var userPublicId))
                {
                    return new VerifyOtpResponseInfo
                    {
                        Status = StatusConstants.Failure,
                        Message = "Invalid user public ID in token."
                    };
                }

                // Extract global role from token
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
                if (roleClaim == null)
                {
                    return new VerifyOtpResponseInfo
                    {
                        Status = StatusConstants.Failure,
                        Message = "Invalid role in token."
                    };
                }

                // Hash the refresh token and validate it
                var tokenHash = _refreshTokenService.HashRefreshToken(refreshToken);
                var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(userPublicId, tokenHash);

                if (!isValid)
                {
                    return new VerifyOtpResponseInfo
                    {
                        Status = StatusConstants.Failure,
                        Message = "Invalid or expired refresh token."
                    };
                }

                // Revoke old refresh token
                await _refreshTokenService.RevokeRefreshTokenAsync(userPublicId, tokenHash);

                // For Rentora: No property context in refresh for global authentication
                // Permissions remain empty at global level
                var permissions = new List<string>();

                // Extract property context claim if this is a property-context token
                // (Advanced: for future implementation of property switching)
                var PropertyUniqueIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "property_public_id");

                // Generate new access token (matching original permissions model)
                var newAccessToken = _jwtTokenService.GenerateToken(userPublicId, roleClaim.Value, permissions);

                // Generate new refresh token
                var newRefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(0, userPublicId);

                long globalRoleId = ExtractRoleIdFromRole(roleClaim.Value);

                return new VerifyOtpResponseInfo
                {
                    Status = StatusConstants.Success,
                    Message = "Token refreshed successfully.",
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken,
                    Permissions = permissions,
                    RoleId = globalRoleId,
                    IsNewUser = false,
                    IsProfileComplete = true
                };
            }
            catch (Exception ex)
            {
                return new VerifyOtpResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = $"Error refreshing token: {ex.Message}"
                };
            }
        }

        public async Task LogoutAsync(Guid userPublicId, string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshToken))
                    return;

                var tokenHash = _refreshTokenService.HashRefreshToken(refreshToken);
                await _refreshTokenService.RevokeRefreshTokenAsync(userPublicId, tokenHash);
            }
            catch (Exception ex)
            {
                // Log exception but don't throw - logout should not fail
            }
        }

        public async Task LogoutAllAsync(Guid userPublicId)
        {
            try
            {
                await _refreshTokenService.RevokeAllRefreshTokensAsync(userPublicId);
            }
            catch (Exception ex)
            {
                // Log exception but don't throw - logout should not fail
            }
        }

        private long ExtractRoleIdFromRole(string role)
        {
            // Try to parse role as number first (in case it contains the ID)
            if (long.TryParse(role, out var roleId))
            {
                return roleId;
            }

            // Otherwise, map role string to internal role IDs
            return role switch
            {
                "PropertyOwner" => InternalRoleConstants.PropertyOwner,
                "PropertyManager" => InternalRoleConstants.PropertyManager,
                "Tenant" => InternalRoleConstants.Tenant,
                "SecurityGuard" => InternalRoleConstants.SecurityGuard,
                "Accountant" => InternalRoleConstants.Accountant,
                "MaintenanceStaff" => InternalRoleConstants.MaintenanceStaff,
                "USER" => 2,  // Global user role
                _ => 0
            };
        }

        private async Task<long?> GetUserIdFromPublicIdAsync(Guid userPublicId)
        {
            try
            {
                // Note: This would require a repository method to look up UserId from UserPublicId
                // For now, return null - this should be implemented with actual lookup
                // This is a known limitation that should be addressed by fetching from database
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

