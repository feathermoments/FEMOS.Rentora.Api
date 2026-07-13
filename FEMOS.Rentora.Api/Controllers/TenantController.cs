using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("property-tenants/{propertyId}")]
        public async Task<IActionResult> GetPropertyTenants(int propertyId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var propertyTenants = await _tenantService.GetPropertyTenantsAsync(userPublicId, propertyId);
            return Ok(propertyTenants);
        }

        [HttpGet("details/{propertyId}/{tenantId}")]
        public  async Task<IActionResult> GetPropertyTenantDetails(int propertyId, int tenantId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var propertyTenantDetails = await _tenantService.GetPropertyTenantDetailsAsync(userPublicId, propertyId, tenantId);
            return Ok(propertyTenantDetails);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SavePropertyTenant([FromBody] PropertyTenantRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _tenantService.SavePropertyTenantAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("save-tenant-assignment")]
        public async Task<IActionResult> SavePropertyTenantAssignment([FromBody] PropertyTenantAssignmentRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _tenantService.SavePropertyTenantAssignmentAsync(objRequestInfo);
            return Ok(result);
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
            var result = await _tenantService.SaveRentAgreementAsync(objRequestInfo);
            return Ok(result);
        }
    }
}
