using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class MenuService :IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuResponseInfo> GetUserMenuAsync(Guid UserPublicId, long PropertyId)
        {
            MenuResponseInfo objResponseInfo = new MenuResponseInfo();
            objResponseInfo.objMenus = await _menuRepository.GetUserMenuAsync(UserPublicId, PropertyId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "User menu retrieved successfully.";
            return objResponseInfo;
        }

        public async Task<MenuPermissionResponseInfo> GetUserMenuPermissionsAsync(Guid UserPublicId, long PropertyId)
        {
            MenuPermissionResponseInfo objResponseInfo = new MenuPermissionResponseInfo();
            objResponseInfo.objMenuPermissions = await _menuRepository.GetUserMenuPermissionsAsync(UserPublicId, PropertyId);
            objResponseInfo.Status = StatusConstants.Success;
            objResponseInfo.Message = "User menu permissions retrieved successfully.";
            return objResponseInfo;
        }
    }
}
