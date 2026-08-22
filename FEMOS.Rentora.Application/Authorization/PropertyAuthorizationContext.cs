using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Authorization
{
    /// <summary>
    /// Default implementation of IPropertyAuthorizationContext.
    /// Stores and manages property context and authorization information extracted from JWT.
    /// </summary>
    public class PropertyAuthorizationContext : IPropertyAuthorizationContext
    {
        private Guid? _currentPropertyPublicId;
        private long _currentRoleId;
        private string _currentRoleCode = string.Empty;
        private List<string> _currentPermissions = new List<string>();
        private bool _isValid;

        public Guid? CurrentPropertyPublicId => _currentPropertyPublicId;
        public long CurrentRoleId => _currentRoleId;
        public string CurrentRoleCode => _currentRoleCode;
        public List<string> CurrentPermissions => _currentPermissions;
        public bool IsValid => _isValid;

        /// <summary>
        /// Initializes the context from HTTP header and JWT claims.
        /// 
        /// Process:
        /// 1. Receive property public ID from X-Property-Public-Id header
        /// 2. Find the property in JWT propertyRoles claim
        /// 3. Extract role ID and role code
        /// 4. Look up permissions for that role from JWT rolePermissions claim
        /// 5. Store in context for use during authorization checks
        /// 
        /// Returns false if:
        /// - Property header is missing (null)
        /// - Property not found in JWT propertyRoles
        /// - Role not found in JWT rolePermissions
        /// </summary>
        public bool TryInitializeFromRequest(
            Guid? headerPropertyPublicId,
            List<PropertyRoleDto> propertyRolesFromJwt,
            Dictionary<long, List<string>> rolePermissionsFromJwt)
        {
            _isValid = false;
            _currentPropertyPublicId = null;
            _currentRoleId = 0;
            _currentRoleCode = string.Empty;
            _currentPermissions = new List<string>();

            // Step 1: Validate header property is provided
            if (!headerPropertyPublicId.HasValue || headerPropertyPublicId == Guid.Empty)
            {
                return false;
            }

            // Step 2: Find property in JWT propertyRoles
            var selectedPropertyRole = propertyRolesFromJwt?.FirstOrDefault(pr =>
                pr.PropertyPublicId == headerPropertyPublicId.Value);

            if (selectedPropertyRole == null)
            {
                // Property not found in JWT - user doesn't have access to this property
                return false;
            }

            // Step 3: Extract role information
            _currentPropertyPublicId = selectedPropertyRole.PropertyPublicId;
            _currentRoleId = selectedPropertyRole.RoleId;
            _currentRoleCode = selectedPropertyRole.RoleCode;

            // Step 4: Look up permissions for the role
            if (rolePermissionsFromJwt != null &&
                rolePermissionsFromJwt.TryGetValue(_currentRoleId, out var permissions))
            {
                _currentPermissions = permissions ?? new List<string>();
            }

            _isValid = true;
            return true;
        }

        /// <summary>
        /// Checks if the user has a specific permission for the current property.
        /// </summary>
        public bool HasPermission(string permissionCode)
        {
            if (!_isValid || string.IsNullOrEmpty(permissionCode))
            {
                return false;
            }

            return _currentPermissions.Contains(permissionCode);
        }
    }
}
