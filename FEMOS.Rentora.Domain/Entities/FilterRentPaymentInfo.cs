using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class FilterRentPaymentInfo : BaseFilterInfo
    {
        public int? InvoiceStatusId { get; set; }
        public int? PaymentStatusId { get; set; }
    }
}
