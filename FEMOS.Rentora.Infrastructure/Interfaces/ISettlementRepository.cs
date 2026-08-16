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
    public interface ISettlementRepository
    {
        Task<MoveOutSettlementResponseInfo> CreateSettlementAsync(MoveOutSettlementRequestInfo objRequestInfo);
        Task<MoveOutSettlementResponseInfo> GetSettlementDetailsAsync(Guid userPublicId, Guid uniqueId);
        Task<FilterResponseInfo> GetPendingActionsAsync(FilterRequestInfo objRequestInfo);
        Task<FilterResponseInfo> GetSettlementsAsync(FilterRequestInfo objRequestInfo);
        Task<BaseResponseInfo> ApproveSettlementAsync(Guid uniqueId, Guid userPublicId);
        Task<BaseResponseInfo> ApprovePaymentAsync(MoveOutSettlementActionRequestInfo objRequestInfo);
        Task<BaseResponseInfo> RejectPaymentAsync(MoveOutSettlementActionRequestInfo objRequestInfo);
        Task<BaseResponseInfo> ConfirmRefundAsync(MoveOutSettlementActionRequestInfo objRequestInfo);
        Task<BaseResponseInfo> MarkRefundPaidAsync(MoveOutSettlementRefundRequestInfo objRequestInfo);
        Task<BaseResponseInfo> RejectRefundAsync(MoveOutSettlementActionRequestInfo objRequestInfo);
    }
}
