using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class CityInfo
    {
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
