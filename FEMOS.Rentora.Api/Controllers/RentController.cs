using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;
        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost("save-rent-agreement")]
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
            var result = await _rentService.SaveRentAgreementAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpGet("agreement-details/{tenantAssignmentId}")]
        public async Task<IActionResult> GetRentAgreement(long tenantAssignmentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentService.GetRentAgreementAsync(userPublicId, tenantAssignmentId);
            return Ok(rentAgreement);
        }

        [HttpDelete("delete-rent-agreement/{rentAgreementId}/{tenantAssignmentId}")]
        public async Task<IActionResult> DeleteRentAgreement(long rentAgreementId, long tenantAssignmentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _rentService.DeleteRentAgreementAsync(userPublicId, rentAgreementId, tenantAssignmentId);
            return Ok(result);
        }

        [HttpPost("get-rent-invoices")]
        public async Task<IActionResult> GetRentInvoices(FilterRentInvoiceRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentService.GetRentInvoicesAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpGet("get-rent-invoice-details/{propertyId}/{rentInvoiceId}")]
        public async Task<IActionResult> GetRentInvoiceDetailsAsync(long propertyId, long rentInvoiceId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentService.GetRentInvoiceDetailsAsync(userPublicId, propertyId, rentInvoiceId);
            return Ok(rentAgreement);
        } 
    }
}