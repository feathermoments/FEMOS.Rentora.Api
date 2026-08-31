using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MemberInfo
    {
        public Guid MemberUserPublicId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string MobileHash { get; set; } = string.Empty;
        public string MobileEncrypted { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string EmailHash { get; set; } = string.Empty;
        public string EmailEncrypted { get; set; } = string.Empty;
        public int GenderId { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string ProfilePhoto { get; set; } = string.Empty;
    }
}
