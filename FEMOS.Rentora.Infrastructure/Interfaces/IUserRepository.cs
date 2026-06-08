using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<UserProfileResponseInfo?> GetUserProfileAsync(Guid userPublicId);

        Task<DBResponseInfo> UpdateUserProfileAsync(UserProfileInfo model);

        Task<DBResponseInfo> DeleteUserAccountAsync(Guid userPublicId);
    }
}
