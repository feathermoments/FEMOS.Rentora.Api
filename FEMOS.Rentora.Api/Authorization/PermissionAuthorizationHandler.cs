using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Authorization
{
    /// <summary>
    /// Authorization handler that checks if the user has the required permission.
    /// Reads permission claims from JWT and validates them against the endpoint requirement.
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<RequirePermissionAttribute>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequirePermissionAttribute requirement)
        {
            // Check if user is authenticated
            if (!context.User.Identity?.IsAuthenticated ?? false)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // Get all permission claims from JWT
            var userPermissions = context.User.FindAll("permission");

            // Check if user has the required permission
            if (userPermissions.Any(p => p.Value == requirement.PermissionCode))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}

