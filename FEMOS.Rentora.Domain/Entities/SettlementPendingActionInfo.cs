using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class SettlementPendingActionInfo
    {
        public Guid UniqueId { get; set; }
        public string UniqueIdDisplay { get; set; }
        public long SettlementId { get; set; }
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
        public decimal FinalDepositBalance { get; set; }
        public int SettlementStatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long SettlementPaymentId { get; set; }
        public int SettlementPaymentStatusId { get; set; }
        public decimal SettlementPaymentAmount { get; set; }
        public long RefundPaymentId { get; set; }
        public int RefundPaymentStatusId { get; set; }
        public decimal RefundPaymentAmount { get; set; }
        public bool VerifiedTenantPayment { get; set; }
        public bool ApprovalTenantPayment { get; set; }
        public decimal RemainingTenantPayable { get; set; }
        public bool VerifiedRefund { get; set; }
        public bool ApprovalRefund { get; set; }
        public decimal RemainingRefund { get; set; }
        public string ActionCode { get; set; }
        public string ActionTitle { get; set; }
        public string ActionMessage { get; set; }
        public bool CanApproveSettlement { get; set; }
        public bool CanCancelSettlement { get; set; }
        public bool CanPaySettlement { get; set; }
        public bool CanApproveTenantPayment { get; set; }
        public bool CanRejectTenantPayment { get; set; }
        public bool CanMarkRefundPaid { get; set; }
        public bool CanConfirmRefund { get; set; }
        public bool CanRejectRefund { get; set; }
        public bool CanFinalizeSettlement { get; set; }
        public bool HasPendingAction { get; set; }
        public long RentPaymentId { get; set; }
    }
}
