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
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("userPublicId", userPublicId.ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, userPublicId.ToString()),
            new(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config[AppSettingConstants.JwtSecretKey]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Replace GetValue<T> with manual parsing since GetValue<T> is not available
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
    }

}
