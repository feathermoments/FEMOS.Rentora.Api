using FEMOS.Rentora.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class VerifyOtpResponseInfo : BaseResponseInfo
    {
        public string Token { get; set; } = string.Empty;
        public bool IsNewUser { get; set; }
        public bool IsProfileComplete { get; set; }
    }
}
