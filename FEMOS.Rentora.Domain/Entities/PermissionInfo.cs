using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PermissionInfo
    {
        public long PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string Module { get; set; }
    }
}

