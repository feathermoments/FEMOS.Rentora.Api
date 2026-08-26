using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IRentAgreementService
    {
        Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo);
        Task<RentAgreementResponseInfo> GetRentAgreementAsync(Guid userPublicId, Guid tenantAssignmentPublicId);
        Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, Guid rentAgreementPublicId, Guid tenantAssignmentPublicId);
        Task<FilterResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo);
        Task<RentAgreementTerminationRequestResponseInfo> CreateTerminationRequestAsync(CreateRentAgreementTerminationRequestInfo objRequestInfo);
        Task<FilterResponseInfo> GetTerminationRequestsAsync(FilterRequestInfo objRequestInfo);
        Task<RentAgreementTerminationRequestResponseInfo> GetTerminationRequestDetailsAsync(Guid userPublicId, Guid rentAgreementPublicId, Guid terminationRequestPublicId);
        Task<BaseResponseInfo> ApproveTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo);
        Task<BaseResponseInfo> RejectTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo);
        Task<BaseResponseInfo> CancelTerminationRequestAsync(TerminationRequestActionInfo objRequestInfo);
    }
}
