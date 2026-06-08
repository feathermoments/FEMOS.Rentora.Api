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
    public interface ITermsRepository
    {
        Task<TermsInfo?> GetCurrentTermsAsync(string appCode, string termsType);
        Task<TermsStatusResponseInfo?> CheckUserTermsStatusAsync(string appCode, string termsType, Guid userPublicId);
        Task<AcceptTermsResponseInfo> AcceptTermsAsync(AcceptTermsRequestInfo model);
        Task<ValidateTermsResponseInfo> ValidateUserTermsAsync(string appCode, Guid userPublicId);
    }
}
