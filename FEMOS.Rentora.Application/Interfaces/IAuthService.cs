using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IAuthService
    {
        Task<SendOtpResponseInfo> SendOtpAsync(SendOtpInfo model);
        Task<VerifyOtpResponseInfo> VerifyOtpAsync(VerifyOtpInfo model);
    }
}
