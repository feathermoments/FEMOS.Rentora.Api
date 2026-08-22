using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Authorization
{
    /// <summary>
    /// Interface for property context and authorization resolution.
    /// This service is called during request processing to:
    /// 1. Extract the selected property from X-Property-Public-Id header
    /// 2. Resolve the user's role for that property from JWT
    /// 3. Resolve permissions for that role from JWT
    /// 4. Provide authorization context to controllers and services
    /// </summary>
    public interface IPropertyAuthorizationContext
    {
        /// <summary>
        /// The currently selected property public ID from X-Property-Public-Id header.
        /// Null if header not provided or invalid.
        /// </summary>
        Guid? CurrentPropertyPublicId { get; }

        /// <summary>
        /// The role ID for the user on the currently selected property.
        /// Derived from JWT propertyRoles claim.
        /// </summary>
        long CurrentRoleId { get; }

        /// <summary>
        /// The role code for the user on the currently selected property (e.g., "OWNER", "TENANT").
        /// Derived from JWT propertyRoles claim.
        /// </summary>
        string CurrentRoleCode { get; }

        /// <summary>
        /// The list of permission codes for the current role.
        /// Derived from JWT rolePermissions claim.
        /// </summary>
        List<string> CurrentPermissions { get; }

        /// <summary>
        /// Checks if the user has a specific permission for the current property.
        /// Uses only JWT data - no database queries.
        /// </summary>
        bool HasPermission(string permissionCode);

        /// <summary>
        /// Initializes the context from HTTP request (header + JWT claims).
        /// Called early in request processing.
        /// Returns false if property header is missing or invalid.
        /// </summary>
        bool TryInitializeFromRequest(
            Guid? headerPropertyPublicId,
            List<PropertyRoleDto> propertyRolesFromJwt,
            Dictionary<long, List<string>> rolePermissionsFromJwt);

        /// <summary>
        /// Determines if the context is valid for authorization checks.
        /// A valid context has a selected property and corresponding role in JWT.
        /// </summary>
        bool IsValid { get; }
    }

    /// <summary>
    /// DTO for property role information from JWT
    /// </summary>
    public class PropertyRoleDto
    {
        public Guid PropertyPublicId { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; } = string.Empty;
    }
}
