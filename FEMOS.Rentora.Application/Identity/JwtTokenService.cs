using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
            return GenerateToken(userPublicId, role, new List<string>());
        }

        public string GenerateToken(Guid userPublicId, string role, List<string> permissions)
        {
            return GenerateToken(userPublicId, role, permissions, null, null, null);
        }

        public string GenerateToken(Guid userPublicId, string role, List<string> permissions, long? propertyId, long? propertyRoleId, string propertyRoleCode)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("userPublicId", userPublicId.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userPublicId.ToString()),
                new(ClaimTypes.Role, role)
            };

            // Add property context if available
            if (propertyId.HasValue)
            {
                claims.Add(new Claim("property_id", propertyId.Value.ToString()));

                if (propertyRoleId.HasValue)
                {
                    claims.Add(new Claim("property_role_id", propertyRoleId.Value.ToString()));
                }

                if (!string.IsNullOrEmpty(propertyRoleCode))
                {
                    claims.Add(new Claim("property_role_code", propertyRoleCode));
                }
            }

            // Add permission claims (for property context, these are property-specific permissions)
            if (permissions != null && permissions.Count > 0)
            {
                foreach (var permission in permissions)
                {
                    claims.Add(new Claim("permission", permission));
                }
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config[AppSettingConstants.JwtSecretKey]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Replace GetValue<T> with manual parsing since GetValue<T> is not available
            var expiryTimeString = _config[AppSettingConstants.JwtExpiryTime];
            int expiryMinutes = 30; // Short-lived access token default
            if (!string.IsNullOrEmpty(expiryTimeString)
                && int.TryParse(expiryTimeString, out var parsedExpiry))
            {
                expiryMinutes = parsedExpiry;
            }
            var tokenExpiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                issuer: _config[AppSettingConstants.JwtIssuer],
                audience: _config[AppSettingConstants.JwtAudience],
                claims: claims,
                expires: tokenExpiryTime,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
