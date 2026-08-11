using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class UtilityService : IUtilityService
    {
        private readonly IUtilityRepository _utilityRepository;
        public UtilityService(IUtilityRepository utilityRepository)
        {
            _utilityRepository = utilityRepository;
        }

        public async Task<UtilityChargeResponseInfo> SaveUtilityChargeAsync(UtilityChargeRequestInfo objRequestInfo)
        {
            return await _utilityRepository.SaveUtilityChargeAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> DeleteUtilityChargeAsync(Guid userPublicId, Guid utilityChargeUniqueId)
        {
            return await _utilityRepository.DeleteUtilityChargeAsync(userPublicId, utilityChargeUniqueId);
        }

        public async Task<UtilityChargeResponseInfo> GetUtilityChargeDetailsAsync(Guid userPublicId, Guid utilityChargeUniqueId)
        {
            return await _utilityRepository.GetUtilityChargeDetailsAsync(userPublicId, utilityChargeUniqueId);
        }

        public async Task<FilterUtilityChargeResponseInfo> GetUtilityChargesAsync(FilterRequestInfo objRequestInfo)
        {
            return await _utilityRepository.GetUtilityChargesAsync(objRequestInfo);
        }
    }
}
