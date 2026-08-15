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
        Task<RentAgreementResponseInfo> GetRentAgreementAsync(Guid userPublicId, long tenantAssignmentId);
        Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, long rentAgreementId, long tenantAssignmentId);
        Task<FilterRentAgreementResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo);
    }
}
