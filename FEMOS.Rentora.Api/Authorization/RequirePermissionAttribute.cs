using Microsoft.AspNetCore.Authorization;
using System;

namespace FEMOS.Rentora.Api.Authorization
{
    /// <summary>
    /// Attribute to enforce permission-based authorization on API endpoints.
    /// Usage: [RequirePermission("PROPERTY.EDIT")]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequirePermissionAttribute : Attribute, IAuthorizationRequirement
    {
        public string PermissionCode { get; }

        public RequirePermissionAttribute(string permissionCode)
        {
            PermissionCode = permissionCode;
        }
    }
}

