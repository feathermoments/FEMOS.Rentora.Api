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
    internal class SettlementService : ISettlementService
    {
        private readonly ISettlementRepository _moveOutSettlementRepository;
        public SettlementService(ISettlementRepository moveOutSettlementRepository)
        {
            _moveOutSettlementRepository = moveOutSettlementRepository;
        }

        public async Task<MoveOutSettlementResponseInfo> CreateSettlementAsync(MoveOutSettlementRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.CreateSettlementAsync(objRequestInfo);
        }

        public async Task<MoveOutSettlementResponseInfo> GetSettlementDetailsAsync(Guid userPublicId, Guid uniqueId)
        {
            return await _moveOutSettlementRepository.GetSettlementDetailsAsync(userPublicId, uniqueId);
        }

        public async Task<FilterResponseInfo> GetPendingActionsAsync(FilterRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.GetPendingActionsAsync(objRequestInfo);
        }

        public async Task<FilterResponseInfo> GetSettlementsAsync(FilterRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.GetSettlementsAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> ApproveSettlementAsync(Guid uniqueId, Guid userPublicId)
        {
            return await _moveOutSettlementRepository.ApproveSettlementAsync(uniqueId, userPublicId);
        }

        public async Task<BaseResponseInfo> ApprovePaymentAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.ApprovePaymentAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> RejectPaymentAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.RejectPaymentAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> ConfirmRefundAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.ConfirmRefundAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> MarkRefundPaidAsync(MoveOutSettlementRefundRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.MarkRefundPaidAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> RejectRefundAsync(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            return await _moveOutSettlementRepository.RejectRefundAsync(objRequestInfo);
        }
    }
}
