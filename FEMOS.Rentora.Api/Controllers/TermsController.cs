using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/terms")]
    [ApiController]
    [Authorize]
    public class TermsController : ControllerBase
    {
        private readonly ITermsService _termsService;

        public TermsController(ITermsService termsService)
        {
            _termsService = termsService;
        }

        /// <summary>GET /api/terms/current?appCode=&termsType=</summary>
        [HttpGet("current")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrent([FromQuery] string appCode, [FromQuery] string termsType)
        {
            if (string.IsNullOrWhiteSpace(appCode) || string.IsNullOrWhiteSpace(termsType))
                return BadRequest(new { message = "appCode and termsType are required." });

            var result = await _termsService.GetCurrentTermsAsync(appCode, termsType);
            if (result is null)
                return NotFound();

            return Ok(new
            {
                version = result.Version,
                title = result.Title,
                content = result.Content,
                isMajorUpdate = result.IsMajorUpdate,
                effectiveFrom = result.EffectiveFrom
            });
        }

        /// <summary>GET /api/terms/status?appCode=&termsType=</summary>
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus([FromQuery] string appCode, [FromQuery] string termsType)
        {
            if (string.IsNullOrWhiteSpace(appCode) || string.IsNullOrWhiteSpace(termsType))
                return BadRequest(new { message = "appCode and termsType are required." });

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var status = await _termsService.CheckUserTermsStatusAsync(appCode, termsType, userPublicId);
            if (status is null)
                return NotFound();

            return Ok(new
            {
                currentVersion = status.CurrentVersion,
                userVersion = status.UserVersion,
                isAcceptanceRequired = status.IsAcceptanceRequired
            });
        }

        /// <summary>POST /api/terms/accept</summary>
        [HttpPost("accept")]
        public async Task<IActionResult> Accept([FromBody] AcceptTermsRequestInfo model)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            model.UserPublicId = userPublicId;
            var result = await _termsService.AcceptTermsAsync(model);
            return Ok(new { success = result.Success, message = result.Message });
        }

        /// <summary>GET /api/terms/validate?appCode=</summary>
        [HttpGet("validate")]
        public async Task<IActionResult> Validate([FromQuery] string appCode)
        {
            if (string.IsNullOrWhiteSpace(appCode))
                return BadRequest(new { message = "appCode is required." });

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _termsService.ValidateUserTermsAsync(appCode, userPublicId);
            return Ok(new { isValid = result.IsValid, message = result.Message });
        }
    }
}
