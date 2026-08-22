using Microsoft.AspNetCore.Authorization;
using FEMOS.Rentora.Application.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Authorization
{
    /// <summary>
    /// Attribute to require a specific permission for an API endpoint.
    /// 
    /// Usage:
    /// [RequirePermission("PROPERTY.VIEW")]
    /// [HttpGet("my-properties")]
    /// public async Task<IActionResult> GetMyProperties(...)
    /// 
    /// The authorization flow:
    /// 1. Check if authentication is present
    /// 2. Resolve IPropertyAuthorizationContext from DI
    /// 3. Check if context is valid (property context exists)
    /// 4. Check if user has the required permission
    /// 5. Return 403 Forbidden if any check fails
    /// 
    /// Note: For endpoints that don't require property context (global endpoints),
    /// use [AllowAnonymous] or [Authorize] without this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _requiredPermission;
        private readonly bool _requirePropertyContext;

        /// <summary>
        /// Creates a permission requirement.
        /// </summary>
        /// <param name="requiredPermission">The permission code required (e.g., "PROPERTY.VIEW")</param>
        /// <param name="requirePropertyContext">If true, X-Property-Public-Id header is required. Default: true</param>
        public RequirePermissionAttribute(string requiredPermission, bool requirePropertyContext = true)
        {
            _requiredPermission = requiredPermission ?? throw new ArgumentNullException(nameof(requiredPermission));
            _requirePropertyContext = requirePropertyContext;
        }

        /// <summary>
        /// Executes authorization filter to check if user has required permission.
        /// </summary>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Validate that user is authenticated
            if (context.HttpContext.User == null || !context.HttpContext.User.Identity?.IsAuthenticated == true)
            {
                context.Result = new ForbidResult("User is not authenticated.");
                return;
            }

            // Get IPropertyAuthorizationContext from DI
            var authorizationContext = context.HttpContext.RequestServices.GetService(typeof(IPropertyAuthorizationContext)) as IPropertyAuthorizationContext;

            if (authorizationContext == null)
            {
                // Authorization context not available - deny access
                context.Result = new ForbidResult("Authorization context is not available.");
                return;
            }

            // Check if property context is required and valid
            if (_requirePropertyContext)
            {
                if (!authorizationContext.IsValid)
                {
                    context.Result = new ForbidResult("Property context is required. Please provide required header.");
                    return;
                }
                // Check if user has the required permission
                if (!authorizationContext.HasPermission(_requiredPermission))
                {
                    context.Result = new ForbidResult($"User does not have permission '{_requiredPermission}' for this action.");
                    return;
                }
            }
            // Authorization passed
            await Task.CompletedTask;
        }
    }
}

