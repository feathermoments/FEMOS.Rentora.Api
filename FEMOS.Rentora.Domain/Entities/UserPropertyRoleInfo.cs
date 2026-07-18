using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UserPropertyMemberInfo
    {
        public long PropertyMemberId { get; set; }
        public long PropertyId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
