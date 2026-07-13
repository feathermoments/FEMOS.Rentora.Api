using FEMOS.Rentora.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UserProfileInfo : BaseInfo
    {
        public string? Name { get; set; }
        public string? EmailAddress { get; set; } = string.Empty;
        public string? MobileNumber { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }

        public string? EmailHash { get; set; }
        public string? EmailEncrypted { get; set; }
        public string? MobileHash { get; set; }
        public string? MobileEncrypted { get; set; }
    }
    
}
