using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        string HashRefreshToken(string token);
        Task<bool> ValidateRefreshTokenAsync(Guid userPublicId, string tokenHash);
        Task<string> CreateRefreshTokenAsync(long userId, Guid userPublicId, string createdByIp = null);
        Task RevokeRefreshTokenAsync(Guid userPublicId, string tokenHash);
        Task RevokeAllRefreshTokensAsync(Guid userPublicId);
    }
}

