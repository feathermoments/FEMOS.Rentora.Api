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
        Task<List<MenuInfo>> GetUserMenuAsync(Guid UserPublicId, long PropertyId);
        Task<List<MenuPermissionInfo>> GetUserMenuPermissionsAsync(Guid UserPublicId, long PropertyId);
    }
}
