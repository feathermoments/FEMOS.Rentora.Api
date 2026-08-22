using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    /// <summary>
    /// Service for loading and building authorization data.
    /// This is called during login and token refresh to build JWT claims
    /// with property roles and permissions.
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationService(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        /// <summary>
        /// Loads authorization data for a user.
        /// 
        /// Process:
        /// 1. Load all properties the user has access to (from Gen_PropertyMembers)
        /// 2. Get the distinct list of role IDs
        /// 3. Load permissions for each distinct role (from Mst_RolePermissions)
        /// 4. Return combined authorization data
        /// 
        /// This ensures:
        /// - No duplicate role permission queries (one query per role, not per property)
        /// - JWT is compact (permissions listed once per role)
        /// - Authorization checks are fast (in-memory lookups only)
        /// </summary>
        public async Task<AuthorizationDataResponseInfo> LoadUserAuthorizationAsync(Guid userPublicId)
        {
            var result = new AuthorizationDataResponseInfo
            {
                UserPublicId = userPublicId,
                PropertyRoles = new List<PropertyRoleDto>(),
                RolePermissions = new List<RolePermissionsDto>()
            };

            // Step 1: Load user's property memberships and roles
            var propertyRoles = await _authorizationRepository.GetUserPropertyRolesAsync(userPublicId);

            if (propertyRoles == null || propertyRoles.Count == 0)
            {
                // User has no property memberships
                return result;
            }

            // Convert to DTOs and store in result
            result.PropertyRoles = propertyRoles.Select(pr => new PropertyRoleDto
            {
                PropertyPublicId = pr.PropertyPublicId,
                RoleId = pr.RoleId,
                RoleCode = pr.RoleCode
            }).ToList();

            // Step 2: Get distinct role IDs (to avoid duplicate permission queries)
            var distinctRoleIds = propertyRoles
                .Select(pr => pr.RoleId)
                .Distinct()
                .ToList();

            if (distinctRoleIds.Count == 0)
            {
                return result;
            }

            // Step 3: Load permissions for all distinct roles efficiently
            var rolePermissionMap = await _authorizationRepository.GetRolePermissionsAsync(distinctRoleIds);

            // Step 4: Build role permissions DTOs
            foreach (var roleId in distinctRoleIds)
            {
                // Find a property role to get the role code
                var roleInfo = propertyRoles.FirstOrDefault(pr => pr.RoleId == roleId);
                if (roleInfo == null) continue;

                var permissions = rolePermissionMap.ContainsKey(roleId)
                    ? rolePermissionMap[roleId]
                    : new List<string>();

                result.RolePermissions.Add(new RolePermissionsDto
                {
                    RoleId = roleId,
                    RoleCode = roleInfo.RoleCode,
                    Permissions = permissions
                });
            }

            return result;
        }
    }
}
