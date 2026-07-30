namespace FEMOS.Rentora.Domain.Entities
{
    public class SecurityDepositSummaryInfo
    {
        public decimal TotalDepositHeld { get; set; }
        public decimal PendingCollection { get; set; }
        public int PendingCollectionCount { get; set; }
        public int PendingApprovalCount { get; set; }
        public decimal RefundDue { get; set; }
    }
}
