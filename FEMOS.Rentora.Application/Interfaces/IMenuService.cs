using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IMenuService
    {
        Task<MenuResponseInfo> GetUserMenuAsync(Guid UserPublicId, long PropertyId);
        Task<MenuPermissionResponseInfo> GetUserMenuPermissionsAsync(Guid UserPublicId, long PropertyId);
    }
}
