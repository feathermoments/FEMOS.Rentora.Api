using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyMemberInfo : MemberUserInfo
    {
        public Guid PropertyPublicId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public long WorkspaceId { get; set; }
        public DateTime? MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
