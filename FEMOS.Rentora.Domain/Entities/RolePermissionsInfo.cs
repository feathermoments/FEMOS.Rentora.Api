using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    /// <summary>
    /// Represents permissions for a specific role.
    /// Extracted from Mst_RolePermissions and included in JWT token.
    /// </summary>
    public class RolePermissionsInfo
    {
        /// <summary>
        /// The internal role ID from the database
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// The role code/name (e.g., "OWNER", "TENANT", "MANAGER")
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// List of permission codes for this role (e.g., "PROPERTY.VIEW", "PROPERTY.EDIT")
        /// </summary>
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
