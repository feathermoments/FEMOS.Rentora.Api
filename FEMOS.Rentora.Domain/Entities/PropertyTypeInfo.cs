using FEMOS.Rentora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyTypeInfo
    {
        public int PropertyTypeId { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public List<PropertyUnitTypeInfo> objPropertyUnitTypes { get; set; } = new List<PropertyUnitTypeInfo>();
    }
}
