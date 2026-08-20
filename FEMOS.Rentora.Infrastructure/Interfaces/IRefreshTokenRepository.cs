using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenInfo> ValidateRefreshTokenAsync(Guid userPublicId, string tokenHash);
        Task<long> CreateRefreshTokenAsync(long userId, Guid userPublicId, string tokenHash, DateTime expiresOn, string createdByIp = null);
        Task RevokeRefreshTokenAsync(Guid userPublicId, string tokenHash, string revokedReason = null);
        Task RevokeAllRefreshTokensAsync(Guid userPublicId, string revokedReason = null);
    }
}

