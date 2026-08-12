using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UtilityChargeInfo
    {
        public int TotalRecords { get; set; }
        public long UtilityChargeId { get; set; }
        public Guid UniqueId { get; set; }
        public long RentInvoiceId { get; set; }
        public long TenantAssignmentId { get; set; }
        public string? TenantName { get; set; }
        public string? UnitNumber { get; set; }
        public short UtilityTypeId { get; set; }
        public string? UtilityType { get; set; }
        public bool IsMeterBased { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal PreviousReading { get; set; }
        public decimal CurrentReading { get; set; }
        public decimal UnitsConsumed { get; set; }
        public decimal RatePerUnit { get; set; }
        public decimal FixedCharge { get; set; }
        public decimal TotalCharge { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Remarks { get; set; }
        public string? ChargeStatus { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
