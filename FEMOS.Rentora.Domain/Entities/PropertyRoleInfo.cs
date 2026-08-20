using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    /// <summary>
    /// Represents a user's internal role for a specific property.
    /// This is different from the global USER role assigned at OTP login.
    /// </summary>
    public class PropertyRoleInfo
    {
        public long PropertyId { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}

