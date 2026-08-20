using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    /// <summary>
    /// Represents the current property context for a user session.
    /// Contains the selected property and the user's internal role for that property.
    /// </summary>
    public class PropertyContextInfo
    {
        public Guid PropertyUniqueId { get; set; }
        public long PropertyId { get; set; }  // Internal ID, not exposed to API
        public long InternalRoleId { get; set; }
        public string InternalRoleCode { get; set; }
        public string InternalRoleName { get; set; }
    }
}

