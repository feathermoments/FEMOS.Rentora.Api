using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UtilityTypeInfo
    {
        public int UtilityTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsMeterBased { get; set; }
        public int DisplayOrder { get; set; }
    }
}
