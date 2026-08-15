using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RentTerminationRequestStatusInfo
    {
        public int TerminationRequestStatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
    }
}
