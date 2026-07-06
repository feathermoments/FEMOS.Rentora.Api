using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class StateInfo
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
    }
}
