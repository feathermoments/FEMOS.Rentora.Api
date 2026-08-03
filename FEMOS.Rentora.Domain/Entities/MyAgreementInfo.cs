namespace FEMOS.Rentora.Domain.Entities
{
    public class MyAgreementInfo
    {
        public long TenantAssignmentId { get; set; }
        public long RentAgreementId { get; set; }
        public string AgreementNumber { get; set; }
        public string AgreementStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysRemaining { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal RequiredDepositAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal PendingDepositAmount { get; set; }
        public string DepositStatus { get; set; }
        public string PaymentStatusCode { get; set; }
        public int NoticePeriodDays { get; set; }
        public int LockInPeriodDays { get; set; }
        public int RentDueDay { get; set; }
        public string AgreementHealth { get; set; }
        public bool CanRenew { get; set; }
        public string AgreementDocumentUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}