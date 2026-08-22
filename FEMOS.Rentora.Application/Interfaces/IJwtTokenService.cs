using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IJwtTokenService
    {
        /// <summary>
        /// Legacy token generation with simple role claim.
        /// Kept for backward compatibility during transition.
        /// </summary>
        string GenerateToken(Guid userPublicId, string role);

        /// <summary>
        /// Enhanced token generation with property roles and permissions.
        /// This is the new method that includes authorization data for property context.
        /// </summary>
        string GenerateTokenWithAuthorization(
            Guid userPublicId,
            List<PropertyRoleInfo> propertyRoles,
            List<RolePermissionsInfo> rolePermissions);

        string GenerateRefreshToken();
    }
}
