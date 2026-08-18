using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Requests
{
    public class MoveOutSettlementActionRequestInfo : BaseRequestInfo
    {
        public long RentPaymentId { get; set; }
        public string Remarks { get; set; }
    }

    public class MoveOutSettlementCreateRequestInfo : BaseRequestInfo
    {
        public Guid UniqueId { get; set; }
        public RentPaymentInfo objRentPaymentInfo { get; set; }
    }

}
