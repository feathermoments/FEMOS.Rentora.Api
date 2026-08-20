using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface ITokenService
    {
        Task<VerifyOtpResponseInfo> RefreshTokenAsync(string accessToken, string refreshToken);
        Task LogoutAsync(Guid userPublicId, string refreshToken);
        Task LogoutAllAsync(Guid userPublicId);
    }
}

