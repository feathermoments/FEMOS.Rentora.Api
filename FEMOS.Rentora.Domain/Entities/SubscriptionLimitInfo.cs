using System;

namespace FEMOS.Rentora.Domain.Entities
{
    public class SubscriptionLimitInfo
    {
        public Guid PlanLimitPublicId { get; set; }
        public string LimitCode { get; set; }
        public string LimitName { get; set; }
        public int LimitValue { get; set; }
        public bool IsUnlimited { get; set; }
        public int Consumed { get; set; }
        public int Remaining { get; set; }
    }
}
