using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
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
    internal class TermsService : ITermsService
    {
        private readonly ITermsRepository _termsRepository;

        public TermsService(ITermsRepository termsRepository)
        {
            _termsRepository = termsRepository;
        }

        public async Task<TermsInfo?> GetCurrentTermsAsync(string appCode, string termsType)
        {
            return await _termsRepository.GetCurrentTermsAsync(appCode, termsType);
        }

        public async Task<TermsStatusResponseInfo?> CheckUserTermsStatusAsync(string appCode, string termsType, Guid userPublicId)
        {
            return await _termsRepository.CheckUserTermsStatusAsync(appCode, termsType, userPublicId);
        }

        public async Task<AcceptTermsResponseInfo> AcceptTermsAsync(AcceptTermsRequestInfo model)
        {
            return await _termsRepository.AcceptTermsAsync(model);
        }

        public async Task<ValidateTermsResponseInfo> ValidateUserTermsAsync(string appCode, Guid userPublicId)
        {
            return await _termsRepository.ValidateUserTermsAsync(appCode, userPublicId);
        }
    }
}
