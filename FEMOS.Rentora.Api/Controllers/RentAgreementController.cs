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
    }
}

