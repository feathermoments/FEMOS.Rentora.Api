using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IRentAgreementRepository
    {
        Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo);
        Task<RentAgreementInfo> GetRentAgreementAsync(Guid userPublicId, long TenantAssignmentId);
        Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, long RentAgreementId, long TenantAssignmentId);
        Task<FilterResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo);
        Task<RentAgreementTerminationRequestResponseInfo> CreateTerminationRequestAsync(CreateRentAgreementTerminationRequestInfo objRequestInfo);
        Task<FilterResponseInfo> GetTerminationRequestsAsync(FilterRequestInfo objRequestInfo);
        Task<RentAgreementTerminationRequestResponseInfo> GetTerminationRequestDetailsAsync(Guid userPublicId, Guid terminationRequestUniqueId);
        Task<BaseResponseInfo> ApproveTerminationRequestAsync(Guid userPublicId, Guid terminationRequestUniqueId, string actionRemarks);
        Task<BaseResponseInfo> RejectTerminationRequestAsync(Guid userPublicId, Guid terminationRequestUniqueId, string actionRemarks);
        Task<BaseResponseInfo> CancelTerminationRequestAsync(Guid userPublicId, Guid terminationRequestUniqueId, string actionRemarks);
    }
}
