using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MenuInfo
    {
        public int MenuId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string RoutePath { get; set; }
        public string IconName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsBottomMenu { get; set; }
        public List<MenuInfo> objSubMenus { get; set; }
    }
}
