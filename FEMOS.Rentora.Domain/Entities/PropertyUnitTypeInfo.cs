using FEMOS.Rentora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyUnitTypeInfo
    {
        public int PropertyTypeId { get; set; }
        public int UnitTypeId { get; set; }
        public string Name { get; set; }
        public bool IsBHKApplicable { get; set; }
        public bool IsAreaApplicable { get; set; }
        public bool IsBedCapacityApplicable { get; set; }
        public bool IsDefault { get; set; }
        public bool AllowSubUnits { get; set; }
        public int DisplayOrder { get; set; }
    }
}
