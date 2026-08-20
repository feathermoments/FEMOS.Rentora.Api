using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RefreshTokenInfo
    {
        public long RefreshTokenId { get; set; }
        public long UserId { get; set; }
        public Guid UserPublicId { get; set; }
        public string TokenHash { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public long? ReplacedByTokenId { get; set; }
        public string CreatedByIp { get; set; }
        public string RevokedReason { get; set; }
        public bool IsActive { get; set; }
    }
}

