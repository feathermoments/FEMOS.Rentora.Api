using Microsoft.AspNetCore.Http;
using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentAgreementController : ControllerBase
    {
        private readonly IRentAgreementService _rentAgreementService;
        public RentAgreementController(IRentAgreementService rentAgreementService)
        {
            _rentAgreementService = rentAgreementService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveRentAgreement(RentAgreementRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentAgreementService.SaveRentAgreementAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpGet("details/{tenantAssignmentId}")]
        public async Task<IActionResult> GetRentAgreement(long tenantAssignmentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentAgreementService.GetRentAgreementAsync(userPublicId, tenantAssignmentId);
            return Ok(rentAgreement);
        }

        [HttpDelete("delete/{rentAgreementId}/{tenantAssignmentId}")]
        public async Task<IActionResult> DeleteRentAgreement(long rentAgreementId, long tenantAssignmentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _rentAgreementService.DeleteRentAgreementAsync(userPublicId, rentAgreementId, tenantAssignmentId);
            return Ok(result);
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetRentAgreements(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentAgreementService.GetRentAgreementsAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("terminate-request")]
        public async Task<IActionResult> TerminateRentAgreement(CreateRentAgreementTerminationRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentAgreementService.CreateTerminationRequestAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("get-terminate-requests")]
        public async Task<IActionResult> GetTerminationRequests(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentAgreementService.GetTerminationRequestsAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("get-terminate-request-details/{TerminationRequestUniqueId}")]
        public async Task<IActionResult> GetTerminationRequestDetails(Guid terminationRequestUniqueId)
        {
            if (terminationRequestUniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(terminationRequestUniqueId));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _rentAgreementService.GetTerminationRequestDetailsAsync(userPublicId, terminationRequestUniqueId);
            return Ok(result);
        }

        [HttpPost("approve-terminate-request")]
        public async Task<IActionResult> ApproveTerminationRequest(TerminationRequestActionInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.TerminationRequestUniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _rentAgreementService.ApproveTerminationRequestAsync(userPublicId, objRequestInfo.TerminationRequestUniqueId, objRequestInfo.ActionRemarks);
            return Ok(result);
        }

        [HttpPost("reject-terminate-request")]
        public async Task<IActionResult> RejectTerminationRequest(TerminationRequestActionInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.TerminationRequestUniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _rentAgreementService.RejectTerminationRequestAsync(userPublicId, objRequestInfo.TerminationRequestUniqueId, objRequestInfo.ActionRemarks);
            return Ok(result);
        }

        [HttpPost("cancel-terminate-request")]
        public async Task<IActionResult> CancelTerminationRequest(TerminationRequestActionInfo objRequestInfo)
        {
            if (objRequestInfo == null || objRequestInfo.TerminationRequestUniqueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _rentAgreementService.CancelTerminationRequestAsync(userPublicId, objRequestInfo.TerminationRequestUniqueId, objRequestInfo.ActionRemarks);
            return Ok(result);
        }
    }
}

