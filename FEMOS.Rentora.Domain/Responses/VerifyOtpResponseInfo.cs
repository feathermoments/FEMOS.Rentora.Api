using FEMOS.Rentora.Domain.Entities;
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
        public string RefreshToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; } = 1800; // 30 minutes
        public long RoleId { get; set; }  // Global role (2 = USER)
        public List<string> Permissions { get; set; } = new List<string>();  // Empty at login
        public bool IsNewUser { get; set; }
        public bool IsProfileComplete { get; set; }

        /// <summary>
        /// List of properties accessible to the user with their internal role for each property.
        /// Populated after successful login. Used for property selection.
        /// </summary>
        public List<AccessiblePropertyInfo> AccessibleProperties { get; set; } = new List<AccessiblePropertyInfo>();
    }
}

