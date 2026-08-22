using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Identity
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Guid userPublicId, string role)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("userPublicId", userPublicId.ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, userPublicId.ToString()),
            new(ClaimTypes.Role, role)
        };

            return BuildToken(claims);
        }

        /// <summary>
        /// Enhanced token generation that includes property roles and role permissions.
        /// This token contains all authorization information needed for:
        /// - Property context validation (X-Property-Public-Id header)
        /// - Role determination for selected property
        /// - Permission checking without database queries
        /// </summary>
        public string GenerateTokenWithAuthorization(
            Guid userPublicId,
            List<PropertyRoleInfo> propertyRoles,
            List<RolePermissionsInfo> rolePermissions)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("userPublicId", userPublicId.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userPublicId.ToString()),
            };

            // Add property roles as a JSON claim
            // This maps each PropertyPublicId to its RoleId and RoleCode
            if (propertyRoles != null && propertyRoles.Count > 0)
            {
                var propertyRolesJson = JsonSerializer.Serialize(propertyRoles.Select(pr => new
                {
                    propertyPublicId = pr.PropertyPublicId,
                    roleId = pr.RoleId,
                    roleCode = pr.RoleCode
                }).ToList());

                claims.Add(new Claim("propertyRoles", propertyRolesJson));
            }

            // Add role permissions as a JSON claim
            // This maps each RoleId to its list of permissions
            if (rolePermissions != null && rolePermissions.Count > 0)
            {
                var rolePermissionsJson = JsonSerializer.Serialize(rolePermissions.Select(rp => new
                {
                    roleId = rp.RoleId,
                    roleCode = rp.RoleCode,
                    permissions = rp.Permissions
                }).ToList());

                claims.Add(new Claim("rolePermissions", rolePermissionsJson));
            }

            // Add the first role as a legacy claim for backward compatibility
            if (rolePermissions != null && rolePermissions.Count > 0)
            {
                claims.Add(new Claim(ClaimTypes.Role, rolePermissions[0].RoleCode));
            }

            return BuildToken(claims);
        }

        /// <summary>
        /// Helper method to build and sign the JWT token
        /// </summary>
        private string BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config[AppSettingConstants.JwtSecretKey]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Parse expiry time from configuration
            var expiryTimeString = _config[AppSettingConstants.JwtExpiryTime];
            int expiryDays = 7;
            if (!string.IsNullOrEmpty(expiryTimeString)
                && int.TryParse(expiryTimeString, out var parsedExpiry))
            {
                expiryDays = parsedExpiry;
            }
            var tokenExpiryTime = DateTime.UtcNow.AddDays(expiryDays);

            var token = new JwtSecurityToken(
                issuer: _config[AppSettingConstants.JwtIssuer],
                audience: _config[AppSettingConstants.JwtAudience],
                claims: claims,
                expires: tokenExpiryTime,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
    }

}
