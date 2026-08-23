using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MenuInfo>> GetUserMenuAsync(Guid UserPublicId, Guid PropertyPublicId);
        Task<List<MenuPermissionInfo>> GetUserMenuPermissionsAsync(Guid UserPublicId, Guid PropertyPublicId);
    }
}
