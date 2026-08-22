using FEMOS.Rentora.Application.Authorization;
using FEMOS.Rentora.Application.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Middleware
{
    /// <summary>
    /// Middleware to extract and initialize property authorization context from HTTP request.
    /// 
    /// This middleware:
    /// 1. Extracts X-Property-Public-Id header (property context selector)
    /// 2. Parses propertyRoles and rolePermissions from JWT claims
    /// 3. Initializes IPropertyAuthorizationContext for use in controllers
    /// 4. Makes authorization data available to dependency injection
    /// 
    /// Must run AFTER authentication middleware so User principal is populated.
    /// </summary>
    public class PropertyAuthorizationContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _skipPaths;

        public PropertyAuthorizationContextMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            // List of paths that don't require property context (e.g., /api/auth/refresh, /api/user/profile)
            _skipPaths = configuration.GetSection("Authorization:SkipPropertyContextPaths")
                .Get<List<string>>() ?? new List<string>();
        }

        public async Task InvokeAsync(HttpContext context, IPropertyAuthorizationContext authContext)
        {
            var path = context.Request.Path.ToString().ToLowerInvariant();

            // Skip property context initialization for paths that don't require it
            if (_skipPaths.Any(p => path.StartsWith(p)))
            {
                await _next(context);
                return;
            }

            // Only apply for authenticated users
            if (context.User.Identity?.IsAuthenticated == true)
            {
                try
                {
                    // Extract X-Property-Public-Id header (case-insensitive)
                    Guid? propertyPublicId = null;
                    if (context.Request.Headers.TryGetValue("X-Property-Public-Id", out var headerValue))
                    {
                        if (Guid.TryParse(headerValue.ToString(), out var parsedId))
                        {
                            propertyPublicId = parsedId;
                        }
                    }

                    // Parse JWT claims to extract property roles and role permissions
                    var propertyRoles = ExtractPropertyRoles(context.User);
                    var rolePermissions = ExtractRolePermissions(context.User);

                    // Initialize authorization context
                    var initialized = ((PropertyAuthorizationContext)authContext).TryInitializeFromRequest(
                        propertyPublicId,
                        propertyRoles,
                        rolePermissions);

                    // Note: We don't fail here if initialization fails.
                    // Individual endpoints will decide if they require property context.
                    // Endpoints that require property context should check authContext.IsValid
                    // and return 403 Forbidden if invalid.
                }
                catch (Exception ex)
                {
                    // Log the exception if needed
                    System.Diagnostics.Debug.WriteLine($"Authorization context initialization error: {ex.Message}");
                    // Continue processing - let the endpoint decide if context is required
                }
            }

            await _next(context);
        }

        /// <summary>
        /// Extracts propertyRoles from JWT claims.
        /// 
        /// Expected claim format (JSON with camelCase):
        /// [
        ///   { "propertyPublicId": "...", "roleId": 1, "roleCode": "OWNER" },
        ///   { "propertyPublicId": "...", "roleId": 3, "roleCode": "TENANT" }
        /// ]
        /// </summary>
        private List<PropertyRoleDto> ExtractPropertyRoles(ClaimsPrincipal user)
        {
            var propertyRolesClaim = user.FindFirst("propertyRoles")?.Value;

            if (string.IsNullOrEmpty(propertyRolesClaim))
            {
                return new List<PropertyRoleDto>();
            }

            try
            {
                // Use JsonSerializerOptions with PropertyNameCaseInsensitive for camelCase/PascalCase mismatch
                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = false
                };

                var propertyRoles = JsonSerializer.Deserialize<List<PropertyRoleDto>>(propertyRolesClaim, options)
                    ?? new List<PropertyRoleDto>();
                return propertyRoles;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to deserialize propertyRoles: {ex.Message}");
                // If deserialization fails, return empty list
                return new List<PropertyRoleDto>();
            }
        }

        /// <summary>
        /// Extracts rolePermissions from JWT claims.
        /// 
        /// Expected claim format (JSON with camelCase):
        /// [
        ///   { "roleId": 1, "roleCode": "OWNER", "permissions": ["PROPERTY.VIEW", "PROPERTY.EDIT", ...] },
        ///   { "roleId": 3, "roleCode": "TENANT", "permissions": ["RENT.VIEW", "PAYMENT.VIEW"] }
        /// ]
        /// </summary>
        private Dictionary<long, List<string>> ExtractRolePermissions(ClaimsPrincipal user)
        {
            var rolePermissionsClaim = user.FindFirst("rolePermissions")?.Value;

            if (string.IsNullOrEmpty(rolePermissionsClaim))
            {
                return new Dictionary<long, List<string>>();
            }

            try
            {
                // Use JsonSerializerOptions with PropertyNameCaseInsensitive for camelCase/PascalCase mismatch
                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = false
                };

                // Deserialize directly to the DTO list
                var rolePermissionsJson = JsonSerializer.Deserialize<List<RolePermissionClaimDto>>(rolePermissionsClaim, options)
                    ?? new List<RolePermissionClaimDto>();

                var result = new Dictionary<long, List<string>>();

                foreach (var rp in rolePermissionsJson)
                {
                    result[rp.RoleId] = rp.Permissions ?? new List<string>();
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to deserialize rolePermissions: {ex.Message}");
                // If deserialization fails, return empty dictionary
                return new Dictionary<long, List<string>>();
            }
        }

        /// <summary>
        /// DTO for deserializing rolePermissions claim from JWT.
        /// Uses property name case insensitivity to handle camelCase from JWT.
        /// </summary>
        private class RolePermissionClaimDto
        {
            public long RoleId { get; set; }
            public string RoleCode { get; set; } = string.Empty;
            public List<string> Permissions { get; set; } = new List<string>();
        }
    }
}
