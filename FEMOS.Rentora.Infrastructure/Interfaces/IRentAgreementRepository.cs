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
        Task<RentAgreementInfo> GetRentAgreementAsync(Guid userPublicId, Guid TenantAssignmentPublicId);
        Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, Guid RentAgreementPublicId, Guid TenantAssignmentPublicId);
        Task<FilterResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo);
        Task<RentAgreementTerminationRequestResponseInfo> CreateTerminationRequestAsync(CreateRentAgreementTerminationRequestInfo objRequestInfo);
        Task<FilterResponseInfo> GetTerminationRequestsAsync(FilterRequestInfo objRequestInfo);
        Task<RentAgreementTerminationRequestResponseInfo> GetTerminationRequestDetailsAsync(Guid userPublicId, Guid rentAgreementPublicId, Guid terminationRequestPublicId);
        Task<BaseResponseInfo> ApproveTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo);
        Task<BaseResponseInfo> RejectTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo);
        Task<BaseResponseInfo> CancelTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo);
    }
}
