using System;

namespace FEMOS.Rentora.Domain.Entities
{
    public class SubscriptionInfo
    {
        public Guid SubscriptionPublicId { get; set; }
        public int AppId { get; set; }
        public int PlanId { get; set; }
        public string PlanCode { get; set; }
        public string PlanName { get; set; }
        public int BillingTermId { get; set; }
        public string TermCode { get; set; }
        public string TermName { get; set; }
        public string SubscriptionStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsTrial { get; set; }
        public int DaysLeft { get; set; }
        public bool AutoRenew { get; set; }
        public bool IsLifetime { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
