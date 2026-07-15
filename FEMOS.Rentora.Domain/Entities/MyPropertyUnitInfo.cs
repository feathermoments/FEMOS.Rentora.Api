using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MyPropertyUnitInfo
    {
        public long UnitId { get; set; }
        public long PropertyId { get; set; }
        public long UnitNumber { get; set; }
        public long TenantId { get; set; }
        public string FullName { get; set; } = String.Empty;
        public int FloorNo { get; set; }
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; } = String.Empty;
        public int BHKTypeId { get; set; }
        public string BHKTypeName { get; set; } = String.Empty;
        public int MonthlyRent { get; set; }
        public int UnitStatusId { get; set; }
        public string UnitStatusName { get; set; } = String.Empty;
        public bool IsAvailable { get; set; }
        public DateTime AvailableFrom { get; set; }
    }
}
