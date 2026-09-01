using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class TenantInfo : MemberUserInfo
    {
        public long TenantId { get; set; }
        public Guid TenantUserPublicId { get; set; }
        public bool IsAssigned { get; set; }
    }
}
