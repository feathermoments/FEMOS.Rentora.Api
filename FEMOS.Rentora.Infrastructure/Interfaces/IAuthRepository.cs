using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IAuthRepository
    {
        Task<SendOtpResponseInfo> SendOtpAsync(SendOtpInfo model, string OTPCode);
        Task<DBAuthResponseInfo> VerifyOtpAsync(VerifyOtpInfo model);
        Task<bool> SaveRefreshTokenAsync(Guid userPublicId, string refreshToken, int expiryDays);
        Task<RefreshTokenInfo> GetRefreshTokenAsync(Guid userPublicId);
        Task<bool> RevokeRefreshTokenAsync(Guid userPublicId);
        Task<bool> RevokeAllRefreshTokensAsync(Guid userPublicId);
    }
}
