using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UnitTypeInfo
    {
        public int UnitTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsBHKApplicable { get; set; }
        public bool IsAreaApplicable { get; set; }
        public bool IsBedCapacityApplicable { get; set; }
    }
}
