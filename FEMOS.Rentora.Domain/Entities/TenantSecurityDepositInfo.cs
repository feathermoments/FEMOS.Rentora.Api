namespace FEMOS.Rentora.Domain.Entities
{
    public class TenantSecurityDepositInfo
    {
        public long TenantSecurityDepositId { get; set; }
        public Guid UniqueId { get; set; }
        public long RentAgreementId { get; set; }
        public string AgreementNumber { get; set; }
        public long TenantAssignmentId { get; set; }
        public long TenantId { get; set; }
        public string TenantName { get; set; }
        public long UnitId { get; set; }
        public string UnitNumber { get; set; }
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }
        public decimal RequiredDepositAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal CurrentDepositAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public decimal RefundedAmount { get; set; }
        public long DepositStatusId { get; set; }
        public string DepositStatusName { get; set; }
        public string DepositStatusCode { get; set; }
        public DateTime? DepositCollectedOn { get; set; }
        public DateTime? LastTransactionOn { get; set; }
        public DateTime? ClosedOn { get; set; }
        public bool CanAdjustDeposit { get; set; }
        public bool CanRefund { get; set; }
        public bool CanFinalize { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
