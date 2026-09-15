using FEMOS.Rentora.Domain.Entities;
using System.Collections.Generic;

namespace FEMOS.Rentora.Domain.Responses
{
    public class SubscriptionEntitlementsResponseInfo : BaseResponseInfo
    {
        public SubscriptionInfo Subscription { get; set; }
        public List<SubscriptionFeatureInfo> Features { get; set; } = new List<SubscriptionFeatureInfo>();
        public List<SubscriptionLimitInfo> Limits { get; set; } = new List<SubscriptionLimitInfo>();
    }
}
