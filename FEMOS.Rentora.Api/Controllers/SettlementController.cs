using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementController : ControllerBase
    {
        private readonly ISettlementService _moveOutSettlementService;
        public SettlementController(ISettlementService moveOutSettlementService)
        {
            _moveOutSettlementService = moveOutSettlementService;
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateSettlement(MoveOutSettlementRequestInfo objRequestInfo)
        //{
        //    if (objRequestInfo == null)
        //    {
        //        throw new ArgumentNullException(nameof(objRequestInfo));
        //    }
        //    var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
        //    if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
        //        return Unauthorized();
        //    objRequestInfo.UserPublicId = userPublicId;
        //    var result = await _moveOutSettlementService.CreateSettlementAsync(objRequestInfo);
        //    return Ok(result);
        //}

        [HttpGet("details/{uniqueId}")]
        public async Task<IActionResult> GetSettlementDetails(Guid uniqueId)
        {
            if (uniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(uniqueId));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _moveOutSettlementService.GetSettlementDetailsAsync(userPublicId, uniqueId);
            return Ok(result);
        }

        [HttpPost("get-pending-actions")]
        public async Task<IActionResult> GetPendingActions(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.GetPendingActionsAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetSettlements(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.GetSettlementsAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("approve-payment")]
        public async Task<IActionResult> ApprovePayment(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.RentPaymentId <= 0)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.ApprovePaymentAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("reject-payment")]
        public async Task<IActionResult> RejectPayment(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.RentPaymentId <= 0)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.RejectPaymentAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("confirm-refund")]
        public async Task<IActionResult> ConfirmRefund(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.RentPaymentId <= 0)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.ConfirmRefundAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("mark-refund-paid")]
        public async Task<IActionResult> MarkRefundPaid(MoveOutSettlementRefundRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.UniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.MarkRefundPaidAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("reject-refund")]
        public async Task<IActionResult> RejectRefund(MoveOutSettlementActionRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.RentPaymentId <= 0)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _moveOutSettlementService.RejectRefundAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveSettlement(Guid uniqueId)
        {
            if (uniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(uniqueId));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _moveOutSettlementService.ApproveSettlementAsync(uniqueId, userPublicId);
            return Ok(result);
        }
    }
}
