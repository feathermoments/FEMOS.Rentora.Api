using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class FilterRentInvoiceInfo : BaseFilterInfo
    {
        public int? InvoiceStatusId { get; set; }
        public int? PaymentStatusId { get; set; }

        public long? TenantAssignmentId { get; set; }
        public long? RentAgreementId { get; set; }
        public int? BillingYear { get; set; }
        public int? BillingMonth { get; set; }

        public DateTime? FromDueDate { get; set; }
        public DateTime? ToDueDate { get; set; }
        public bool OutstandingOnly { get; set; } = false;
        public bool OverDueOnly { get; set; } = false;

    }
}
