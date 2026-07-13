using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UserAccountInfo
    {
        public long UserId { get; set; }
        public int CountryId { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string MobileHash { get; set; } = string.Empty;
        public string MobileEncrypted { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string EmailHash { get; set; } = string.Empty;
        public string EmailEncrypted { get; set; } = string.Empty;
        public int AccountStatusId { get; set; }
        public int UserRoleId { get; set; }
        public long CreatorUserId { get; set; }
        public string LanguageId { get; set; } = string.Empty;
        public Guid UserPublicId { get; set; }
    }
}
