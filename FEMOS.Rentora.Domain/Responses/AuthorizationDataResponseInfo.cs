using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    /// <summary>
    /// Enhanced login response that includes authorization information
    /// for property context and role permissions.
    /// </summary>
    public class AuthorizationDataResponseInfo
    {
        /// <summary>
        /// User's public identifier
        /// </summary>
        public Guid UserPublicId { get; set; }

        /// <summary>
        /// List of properties the user has access to with their roles
        /// </summary>
        public List<PropertyRoleDto> PropertyRoles { get; set; } = new List<PropertyRoleDto>();

        /// <summary>
        /// Mapping of roles to their permissions (de-duplicated)
        /// </summary>
        public List<RolePermissionsDto> RolePermissions { get; set; } = new List<RolePermissionsDto>();
    }

    /// <summary>
    /// DTO for property role in authorization data
    /// </summary>
    public class PropertyRoleDto
    {
        public Guid PropertyPublicId { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for role permissions in authorization data
    /// </summary>
    public class RolePermissionsDto
    {
        public long RoleId { get; set; }
        public string RoleCode { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
