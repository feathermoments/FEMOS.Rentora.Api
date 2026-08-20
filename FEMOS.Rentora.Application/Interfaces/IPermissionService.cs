using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IPermissionService
    {
        /// <summary>
        /// Get permissions by global role.
        /// For Rentora login, this is RoleId 2 (USER) - returns empty for Rentora.
        /// </summary>
        Task<List<string>> GetUserPermissionsAsync(Guid userPublicId, long roleId);

        /// <summary>
        /// Get all properties accessible to a user with their internal role for each.
        /// Used after successful OTP login to populate property selection.
        /// </summary>
        Task<List<AccessiblePropertyInfo>> GetAccessiblePropertiesAsync(Guid userPublicId);

        /// <summary>
        /// Get property-context: internal role and permissions for a specific property.
        /// Called when user selects a property.
        /// </summary>
        Task<(PropertyContextInfo Context, List<string> PermissionCodes)> GetPropertyPermissionsAsync(
            Guid userPublicId, Guid PropertyUniqueId);

        // Backward compatibility methods
        Task<PropertyContextInfo> GetPropertyContextAsync(Guid userPublicId, long propertyId);
    }
}

