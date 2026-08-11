using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IUtilityService _utilityService;
        public UtilityController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        [HttpPost("save-utility-charge")]
        public async Task<IActionResult> SaveUtilityCharge(UtilityChargeRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _utilityService.SaveUtilityChargeAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpDelete("delete-utility-charge/{utilityChargeUniqueId}")]
        public async Task<IActionResult> DeleteUtilityCharge(Guid utilityChargeUniqueId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _utilityService.DeleteUtilityChargeAsync(userPublicId, utilityChargeUniqueId);
            return Ok(result);
        }

        [HttpGet("get-utility-charge-details/{utilityChargeUniqueId}")]
        public async Task<IActionResult> GetUtilityChargeDetails(Guid utilityChargeUniqueId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _utilityService.GetUtilityChargeDetailsAsync(userPublicId, utilityChargeUniqueId);
            return Ok(result);
        }

        [HttpPost("get-utility-charges")]
        public async Task<IActionResult> GetUtilityCharges(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _utilityService.GetUtilityChargesAsync(objRequestInfo);
            return Ok(result);
        }
    }
}
