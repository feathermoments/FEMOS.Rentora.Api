namespace FEMOS.Rentora.Domain.Entities
{
    public class AgreementInfo
    {
        public long RentAgreementId { get; set; }
        public string AgreementNumber { get; set; }
        public string AgreementStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysRemaining { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public int NoticePeriodDays { get; set; }
        public int RentDueDay { get; set; }
        public string AgreementHealth { get; set; }
        public bool CanRenew { get; set; }
        public string AgreementDocumentUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}