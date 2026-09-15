using FEMOS.Rentora.Domain.Responses;
using System;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<SubscriptionEntitlementsResponseInfo> GetEntitlementsAsync(Guid userPublicId);
    }
}
