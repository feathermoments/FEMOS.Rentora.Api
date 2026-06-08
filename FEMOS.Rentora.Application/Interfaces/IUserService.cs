using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileResponseInfo?> GetUserProfileAsync(Guid userPublicId);

        Task<BaseResponseInfo> UpdateUserProfileAsync(UserProfileInfo model);

        Task<BaseResponseInfo> DeleteUserAccountAsync(Guid userPublicId);
    }
}
