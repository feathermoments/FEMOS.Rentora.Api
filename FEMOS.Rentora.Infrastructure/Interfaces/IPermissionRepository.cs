using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IPermissionRepository
    {
        /// <summary>
        /// Get permissions by global role.
        /// For Rentora, only used for the global USER role at login (should return empty).
        /// </summary>
        Task<List<PermissionInfo>> GetUserPermissionsByRoleAsync(Guid userPublicId, long roleId);

        /// <summary>
        /// Get all properties accessible to a user with their internal role for each property.
        /// </summary>
        Task<List<AccessiblePropertyInfo>> GetAccessiblePropertiesAsync(Guid userPublicId);

        /// <summary>
        /// Get permissions for a user on a specific property.
        /// Returns property context info and permission codes.
        /// </summary>
        Task<(PropertyContextInfo Context, List<string> PermissionCodes)> GetPropertyPermissionsAsync(
            Guid userPublicId, Guid PropertyUniqueId);

        // Backward compatibility methods from previous implementation
        Task<(PropertyRoleInfo PropertyRole, List<PermissionInfo> Permissions)> GetUserPermissionsByPropertyAsync(Guid userPublicId, long propertyId);

        Task<PropertyRoleInfo> GetPropertyRoleInfoAsync(Guid userPublicId, long propertyId);
    }
}

