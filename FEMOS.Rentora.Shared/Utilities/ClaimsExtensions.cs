using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Shared.Utilities
{
    /// <summary>
    /// Extension methods for extracting claims from ClaimsPrincipal
    /// </summary>
    public static class ClaimsExtensions
    {
        /// <summary>
        /// Extracts userPublicId from JWT token claims
        /// </summary>
        /// <param name="user">The ClaimsPrincipal (User) object from HttpContext</param>
        /// <returns>The userPublicId as Guid</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when userPublicId claim is missing or invalid</exception>
        public static Guid GetUserPublicId(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User principal is null.");

            var userPublicIdClaim = user.FindFirst("userPublicId")?.Value;

            if (string.IsNullOrEmpty(userPublicIdClaim))
                throw new UnauthorizedAccessException("userPublicId claim not found in token.");

            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                throw new UnauthorizedAccessException("userPublicId claim is not a valid GUID.");

            return userPublicId;
        }

        /// <summary>
        /// Tries to extract userPublicId from JWT token claims
        /// </summary>
        /// <param name="user">The ClaimsPrincipal (User) object from HttpContext</param>
        /// <param name="userPublicId">The extracted userPublicId</param>
        /// <returns>True if successfully extracted, false otherwise</returns>
        public static bool TryGetUserPublicId(this ClaimsPrincipal user, out Guid userPublicId)
        {
            userPublicId = Guid.Empty;

            try
            {
                userPublicId = user.GetUserPublicId();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extracts user role from JWT token claims
        /// </summary>
        /// <param name="user">The ClaimsPrincipal (User) object from HttpContext</param>
        /// <returns>The user role or "User" as default</returns>
        public static string GetUserRole(this ClaimsPrincipal user)
        {
            if (user == null)
                return "User";

            return user.FindFirst(ClaimTypes.Role)?.Value ?? "User";
        }

        /// <summary>
        /// Extracts user email from JWT token claims
        /// </summary>
        /// <param name="user">The ClaimsPrincipal (User) object from HttpContext</param>
        /// <returns>The user email or empty string if not found</returns>
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            if (user == null)
                return string.Empty;

            return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Extracts user name from JWT token claims
        /// </summary>
        /// <param name="user">The ClaimsPrincipal (User) object from HttpContext</param>
        /// <returns>The user name or empty string if not found</returns>
        public static string GetUserName(this ClaimsPrincipal user)
        {
            if (user == null)
                return string.Empty;

            return user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Extracts property roles from JWT token claims.
        /// Property roles map properties to roles for a user.
        /// 
        /// Expected format:
        /// [
        ///   { "propertyPublicId": "...", "roleId": 1, "roleCode": "OWNER" },
        ///   { "propertyPublicId": "...", "roleId": 3, "roleCode": "TENANT" }
        /// ]
        /// </summary>
        public static List<PropertyRoleClaim> GetPropertyRoles(this ClaimsPrincipal user)
        {
            if (user == null)
                return new List<PropertyRoleClaim>();

            var propertyRolesClaim = user.FindFirst("propertyRoles")?.Value;

            if (string.IsNullOrEmpty(propertyRolesClaim))
                return new List<PropertyRoleClaim>();

            try
            {
                return JsonSerializer.Deserialize<List<PropertyRoleClaim>>(propertyRolesClaim)
                    ?? new List<PropertyRoleClaim>();
            }
            catch
            {
                return new List<PropertyRoleClaim>();
            }
        }

        /// <summary>
        /// Extracts role permissions from JWT token claims.
        /// Role permissions map roles to their allowed actions.
        /// 
        /// Expected format:
        /// [
        ///   { "roleId": 1, "roleCode": "OWNER", "permissions": ["PROPERTY.VIEW", "PROPERTY.EDIT", ...] },
        ///   { "roleId": 3, "roleCode": "TENANT", "permissions": ["RENT.VIEW", "PAYMENT.VIEW"] }
        /// ]
        /// </summary>
        public static Dictionary<long, List<string>> GetRolePermissions(this ClaimsPrincipal user)
        {
            if (user == null)
                return new Dictionary<long, List<string>>();

            var rolePermissionsClaim = user.FindFirst("rolePermissions")?.Value;

            if (string.IsNullOrEmpty(rolePermissionsClaim))
                return new Dictionary<long, List<string>>();

            try
            {
                var rolePermissions = JsonSerializer.Deserialize<List<RolePermissionClaim>>(rolePermissionsClaim)
                    ?? new List<RolePermissionClaim>();

                var result = new Dictionary<long, List<string>>();
                foreach (var rp in rolePermissions)
                {
                    result[rp.RoleId] = rp.Permissions ?? new List<string>();
                }

                return result;
            }
            catch
            {
                return new Dictionary<long, List<string>>();
            }
        }
    }

    /// <summary>
    /// DTO for deserializing propertyRoles claim from JWT
    /// </summary>
    public class PropertyRoleClaim
    {
        public Guid PropertyPublicId { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for deserializing rolePermissions claim from JWT
    /// </summary>
    public class RolePermissionClaim
    {
        public long RoleId { get; set; }
        public string RoleCode { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}

