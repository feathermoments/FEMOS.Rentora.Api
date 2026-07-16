using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
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
    internal class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        public RentService(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }

        public async Task<RentAgreementResponseInfo> GetRentAgreementAsync(Guid userPublicId, long TenantAssignmentId)
        {
            RentAgreementResponseInfo objResponseInfo = new RentAgreementResponseInfo();
            objResponseInfo.objRentAgreementInfo = await _rentRepository.GetRentAgreementAsync(userPublicId, TenantAssignmentId);
            if (objResponseInfo.objRentAgreementInfo != null)
            {
                objResponseInfo.Status = StatusConstants.Success;
                objResponseInfo.Message = "Rent agreement details retrieved successfully.";
            }
            else
            {
                objResponseInfo.objRentAgreementInfo = new RentAgreementInfo()
                {
                    RentAgreementId = 0,
                    TenantAssignmentId = TenantAssignmentId,
                    AgreementStatusId = 4,
                    AgreementStatus = "Draft (Pending)"
                };
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Rent agreement not found. Create a new draft.";
            }
            return objResponseInfo;
        }

        public async Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo)
        {
            return await _rentRepository.SaveRentAgreementAsync(objRequestInfo);
        }
    }
}
