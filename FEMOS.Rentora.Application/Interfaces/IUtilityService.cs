using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IUtilityService
    {
        Task<UtilityChargeResponseInfo> SaveUtilityChargeAsync(UtilityChargeRequestInfo objRequestInfo);
        Task<BaseResponseInfo> DeleteUtilityChargeAsync(Guid userPublicId, Guid utilityChargeUniqueId);
        Task<UtilityChargeResponseInfo> GetUtilityChargeDetailsAsync(Guid userPublicId, Guid utilityChargeUniqueId);
        Task<FilterUtilityChargeResponseInfo> GetUtilityChargesAsync(FilterRequestInfo objRequestInfo);
    }
}
