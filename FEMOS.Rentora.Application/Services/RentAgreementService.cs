using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Constants;
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
    internal class RentAgreementService : IRentAgreementService
    {
        private readonly IRentAgreementRepository _rentAgreementRepository;
        public RentAgreementService(IRentAgreementRepository rentAgreementRepository)
        {
            _rentAgreementRepository = rentAgreementRepository;
        }

        public async Task<RentAgreementResponseInfo> SaveRentAgreementAsync(RentAgreementRequestInfo objRequestInfo)
        {
            return await _rentAgreementRepository.SaveRentAgreementAsync(objRequestInfo);
        }

        public async Task<RentAgreementResponseInfo> GetRentAgreementAsync(Guid userPublicId, long tenantAssignmentId)
        {
            RentAgreementResponseInfo objResponseInfo = new RentAgreementResponseInfo();
            objResponseInfo.objRentAgreementInfo = await _rentAgreementRepository.GetRentAgreementAsync(userPublicId, tenantAssignmentId);
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
                    TenantAssignmentId = tenantAssignmentId,
                    AgreementStatusId = 4,
                    AgreementStatus = "Draft (Pending)"
                };
                objResponseInfo.Status = StatusConstants.Failure;
                objResponseInfo.Message = "Rent agreement not found. Create a new draft.";
            }
            return objResponseInfo;
        }

        public async Task<BaseResponseInfo> DeleteRentAgreementAsync(Guid userPublicId, long rentAgreementId, long tenantAssignmentId)
        {
            return await _rentAgreementRepository.DeleteRentAgreementAsync(userPublicId, rentAgreementId, tenantAssignmentId);
        }

        public async Task<FilterRentAgreementResponseInfo> GetRentAgreementsAsync(FilterRequestInfo objRequestInfo)
        {
            return await _rentAgreementRepository.GetRentAgreementsAsync(objRequestInfo);
        }
    }
}
