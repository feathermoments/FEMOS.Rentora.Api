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
        Task<MenuResponseInfo> GetUserMenuAsync(Guid userPublicId, Guid propertyPublicId);
        Task<MenuPermissionResponseInfo> GetUserMenuPermissionsAsync(Guid userPublicId, Guid propertyPublicId);
    }
}
