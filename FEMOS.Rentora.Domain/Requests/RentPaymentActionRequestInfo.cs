using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Requests
{
    public class RentPaymentActionRequestInfo : BaseRequestInfo
    {
        public long RentPaymentId { get; set; }
        public string ActionTaken { get; set; }
	    public Guid TransactionGuid { get; set; }
    }
}
