using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MoveOutSettlementInfo
    {
        public long SettlementId { get; set; }
        public Guid UniqueId { get; set; }
        public long RentAgreementId { get; set; }
        public long TenantAssignmentId { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal OutstandingRent { get; set; }
        public decimal OutstandingMaintenance { get; set; }
        public decimal UtilityCharges { get; set; }
        public decimal DamageCharges { get; set; }
        public decimal LateFee { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal TotalRecoverable { get; set; }
        public decimal SecurityDepositHeld { get; set; }
        public decimal DepositAdjusted { get; set; }
        public decimal DepositRefund { get; set; }
        public decimal FinalPayableByTenant { get; set; }
        public decimal FinalRefundToTenant { get; set; }
        public string Remarks { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public short SettlementStatusId { get; set; }
        public string SettlementStatus { get; set; }
        public decimal FinalDepositBalance { get; set; }
        public string? ProcessedBy { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int TotalRecords { get; set; }
        public string AgreementNumber { get; set; }
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }
        public long UnitId { get; set; }
        public string UnitNumber { get; set; }
        public long TenantId { get; set; }
        public string TenantName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
