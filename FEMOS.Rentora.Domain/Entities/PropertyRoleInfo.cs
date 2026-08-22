using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    /// <summary>
    /// Represents a property-role mapping for a user.
    /// Extracted from Gen_PropertyMembers and included in JWT token.
    /// </summary>
    public class PropertyRoleInfo
    {
        /// <summary>
        /// The public identifier for the property (not internal ID)
        /// </summary>
        public Guid PropertyPublicId { get; set; }

        /// <summary>
        /// The internal role ID from the database (used for permission lookup)
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// The role code/name (e.g., "OWNER", "TENANT", "MANAGER")
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;
    }
}
