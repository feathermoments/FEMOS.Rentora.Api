using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class MenuResponseInfo : BaseResponseInfo
    {
        public List<MenuInfo> objMenus { get; set; } = new List<MenuInfo>();
    }
}
