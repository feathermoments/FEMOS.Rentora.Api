using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<List<string>> GetUserPermissionsAsync(Guid userPublicId, long roleId)
        {
            // For Rentora: RoleId 2 (USER) at login has no permissions
            // Permissions are loaded per property context
            var permissions = await _permissionRepository.GetUserPermissionsByRoleAsync(userPublicId, roleId);
            var permissionCodes = permissions
                .Select(p => p.PermissionCode)
                .OrderBy(p => p)
                .ToList();

            return permissionCodes;
        }

        /// <summary>
        /// Get all properties accessible to the user with their internal role for each property.
        /// </summary>
        public async Task<List<AccessiblePropertyInfo>> GetAccessiblePropertiesAsync(Guid userPublicId)
        {
            var properties = await _permissionRepository.GetAccessiblePropertiesAsync(userPublicId);
            return properties;
        }

        /// <summary>
        /// Get property-context: internal role and permissions for a specific property.
        /// </summary>
        public async Task<(PropertyContextInfo Context, List<string> PermissionCodes)> GetPropertyPermissionsAsync(
            Guid userPublicId, Guid PropertyUniqueId)
        {
            var (context, permissions) = await _permissionRepository.GetPropertyPermissionsAsync(userPublicId, PropertyUniqueId);
            return (context, permissions);
        }

        // Backward compatibility
        public async Task<PropertyContextInfo> GetPropertyContextAsync(Guid userPublicId, long propertyId)
        {
            var roleInfo = await _permissionRepository.GetPropertyRoleInfoAsync(userPublicId, propertyId);

            if (roleInfo == null)
            {
                return null;
            }

            return new PropertyContextInfo
            {
                PropertyId = propertyId,
                InternalRoleId = roleInfo.RoleId,
                InternalRoleCode = roleInfo.RoleCode,
                InternalRoleName = roleInfo.RoleName
            };
        }
    }
}

