using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class FilterInfo
    {
        public Guid PropertyPublicId { get; set; }
        public Guid? UnitPublicId { get; set; }
        public long? TenantId { get; set; }
        public int? InvoiceStatusId { get; set; }
        public int? PaymentStatusId { get; set; }

        public Guid? TenantAssignmentPublicId { get; set; }
        public Guid? RentAgreementPublicId { get; set; }
        public int? BillingYear { get; set; }
        public int? BillingMonth { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool OutstandingOnly { get; set; } = false;
        public bool OverDueOnly { get; set; } = false;

        public string? SearchText { get; set; } = null;

        // Utility Charges Filter
        public short? UtilityTypeId { get; set; }
        public Guid? RentInvoicePublicId { get; set; }
        public bool? IsInvoiced { get; set; }

        // Termination Request Filter
        public short? TerminationRequestStatusId { get; set; }

        // Move Out Settlement Filter
        public Guid? UniqueId { get; set; }
        public short? SettlementStatusId { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
