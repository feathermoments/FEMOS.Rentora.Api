namespace FEMOS.Rentora.Domain.Entities
{
    public class RecentPaymentInfo
    {
        public int TotalPayments { get; set; }
        public decimal TotalAmount { get; set; }
        public List<RecentPaymentDetailInfo> RecentPayments { get; set; }
    }
    public class RecentPaymentDetailInfo
    {
        public long PaymentId { get; set; }
        public long TenantId { get; set; }
        public string TenantName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string UnitNumber { get; set; }
    }
}
