using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    /// <summary>
    /// Service for loading and building authorization data.
    /// Used during login and token refresh to:
    /// 1. Load user's property memberships and roles
    /// 2. Load permissions for those roles
    /// 3. Build authorization data to include in JWT
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Loads authorization data for a user.
        /// This includes:
        /// - Property -> Role mappings
        /// - Role -> Permission mappings (de-duplicated)
        /// 
        /// Called after successful authentication to build JWT claims.
        /// </summary>
        Task<AuthorizationDataResponseInfo> LoadUserAuthorizationAsync(Guid userPublicId);
    }
}
