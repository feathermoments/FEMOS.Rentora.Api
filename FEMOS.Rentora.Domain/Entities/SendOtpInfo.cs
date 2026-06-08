using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class SendOtpInfo
    {
        public string Identifier { get; set; } = string.Empty;
        public string ContactHash { get; set; } = string.Empty;
        public string ContactEncrypted { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string OtpHash { get; set; } = string.Empty;
        public string OtpEncrypted { get; set; } = string.Empty;
    }
}
