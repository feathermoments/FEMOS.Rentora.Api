using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class BaseFilterInfo
    {
        public long PropertyId { get; set; }
        public long? UnitId { get; set; }
        
        public string? SearchText { get; set; } = null;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
