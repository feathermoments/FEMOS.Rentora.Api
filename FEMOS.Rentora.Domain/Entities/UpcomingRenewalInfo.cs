namespace FEMOS.Rentora.Domain.Entities
{
    public class UpcomingRenewalInfo
    {
        public int ExpiringNext7Days { get; set; }
        public int ExpiringNext30Days { get; set; }
        public int ExpiredAgreements { get; set; }
        public int DraftAgreements { get; set; }
        public decimal RenewalDueValue { get; set; }
        public int ActiveAgreements { get; set; }
        public int TerminatedAgreements { get; set; }
    }
}