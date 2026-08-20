using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _config;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IConfiguration config)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _config = config;
        }

        public string GenerateRefreshToken()
        {
            // Generate a 32-byte (256-bit) random token
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        public string HashRefreshToken(string token)
        {
            // Hash the token using SHA256 for storage
            using (var sha256 = SHA256.Create())
            {
                var hashedToken = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                return Convert.ToBase64String(hashedToken);
            }
        }

        public async Task<bool> ValidateRefreshTokenAsync(Guid userPublicId, string tokenHash)
        {
            var token = await _refreshTokenRepository.ValidateRefreshTokenAsync(userPublicId, tokenHash);
            return token != null && token.IsActive && token.ExpiresOn > DateTime.UtcNow;
        }

        public async Task<string> CreateRefreshTokenAsync(long userId, Guid userPublicId, string createdByIp = null)
        {
            var token = GenerateRefreshToken();
            var tokenHash = HashRefreshToken(token);

            // Parse refresh token expiration from config
            var refreshTokenDaysString = _config["Jwt:RefreshTokenDays"];
            int refreshTokenDays = 7; // Default 7 days
            if (!string.IsNullOrEmpty(refreshTokenDaysString) && int.TryParse(refreshTokenDaysString, out var parsedDays))
            {
                refreshTokenDays = parsedDays;
            }

            var expiresOn = DateTime.UtcNow.AddDays(refreshTokenDays);

            await _refreshTokenRepository.CreateRefreshTokenAsync(userId, userPublicId, tokenHash, expiresOn, createdByIp);

            return token;
        }

        public async Task RevokeRefreshTokenAsync(Guid userPublicId, string tokenHash)
        {
            await _refreshTokenRepository.RevokeRefreshTokenAsync(userPublicId, tokenHash, "Token rotated during refresh");
        }

        public async Task RevokeAllRefreshTokensAsync(Guid userPublicId)
        {
            await _refreshTokenRepository.RevokeAllRefreshTokensAsync(userPublicId, "User logged out");
        }
    }
}

