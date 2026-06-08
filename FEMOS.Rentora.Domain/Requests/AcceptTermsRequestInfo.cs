using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Requests
{
    public class AcceptTermsRequestInfo : BaseRequestInfo
    {
        public string AppCode { get; set; } = string.Empty;
        public int TermsVersion { get; set; }
        public string TermsType { get; set; } = string.Empty;
        public string AcceptedVia { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
    }
}
