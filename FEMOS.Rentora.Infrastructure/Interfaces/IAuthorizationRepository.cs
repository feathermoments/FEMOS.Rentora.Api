using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository for loading authorization-related data from database.
    /// Used during login and token refresh to build JWT claims.
    /// </summary>
    public interface IAuthorizationRepository
    {
        /// <summary>
        /// Loads all properties and roles for a user from Gen_PropertyMembers.
        /// Returns: PropertyPublicId, RoleId, RoleCode for each property.
        /// </summary>
        Task<List<PropertyRoleInfo>> GetUserPropertyRolesAsync(Guid userPublicId);

        /// <summary>
        /// Loads permissions for a specific role from Mst_RolePermissions.
        /// Returns: List of permission codes for the role.
        /// </summary>
        Task<List<string>> GetRolePermissionsAsync(long roleId);

        /// <summary>
        /// Loads permissions for multiple roles efficiently.
        /// Returns: RoleId -> List of permission codes.
        /// </summary>
        Task<Dictionary<long, List<string>>> GetRolePermissionsAsync(List<long> roleIds);
    }
}
