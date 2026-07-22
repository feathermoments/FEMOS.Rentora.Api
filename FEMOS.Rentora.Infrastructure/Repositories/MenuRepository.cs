using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    internal class MenuRepository : IMenuRepository
    {
        private readonly IDBHelper _dbHelper;
        public MenuRepository(IDBHelper dbHelper) 
        { 
            _dbHelper = dbHelper;
        }

        public async Task<List<MenuInfo>> GetUserMenuAsync(Guid UserPublicId, long PropertyId)
        {
            var cmd = new SqlCommand(DBConstants.USP_GetUserMenus);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", PropertyId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            List<MenuInfo> menuList = _dbHelper.ConvertDataTable<MenuInfo>(dt);
            List<MenuInfo> parentMenus = menuList.Where(m => m.IsBottomMenu).ToList();
            parentMenus.AddRange(menuList.Where(m => m.ParentMenuId == 0 && !m.IsBottomMenu).ToList());
            foreach (var parentMenu in parentMenus.Where(x => !x.IsBottomMenu))
            {
                parentMenu.objSubMenus = menuList.Where(m => m.ParentMenuId == parentMenu.MenuId).ToList();
            }
            return parentMenus;
        }

        public async Task<List<MenuPermissionInfo>> GetUserMenuPermissionsAsync(Guid UserPublicId, long PropertyId)
        {
            var cmd = new SqlCommand(DBConstants.USP_GetUserMenuPermissions);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", UserPublicId);
            cmd.Parameters.AddWithValue("@PropertyId", PropertyId);
            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<MenuPermissionInfo>(dt);
        }
    }
}
