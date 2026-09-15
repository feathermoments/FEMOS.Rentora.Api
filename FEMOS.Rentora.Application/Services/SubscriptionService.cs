using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<SubscriptionEntitlementsResponseInfo> GetEntitlementsAsync(Guid userPublicId)
        {
            return await _subscriptionRepository.GetEntitlementsAsync(userPublicId);
        }
    }
}
