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

        [HttpGet("property-tenants/{propertyPublicId}")]
        public async Task<IActionResult> GetPropertyTenants(Guid propertyPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var propertyTenants = await _tenantService.GetPropertyTenantsAsync(userPublicId, propertyPublicId);
            return Ok(propertyTenants);
        }

        [HttpGet("details/{propertyPublicId}/{tenantId}")]
        public  async Task<IActionResult> GetPropertyTenantDetails(Guid propertyPublicId, long tenantId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var propertyTenantDetails = await _tenantService.GetPropertyTenantDetailsAsync(userPublicId, propertyPublicId, tenantId);
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

        [HttpGet("getTenantAssignment/{propertyPublicId}/{tenantId}/{tenantAssignmentPublicId}")]
        public async Task<IActionResult> GetTenantAssignment(Guid propertyPublicId, long tenantId, Guid tenantAssignmentPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var propertyTenantAssignments = await _tenantService.GetTenantAssignmentDetailsAsync(userPublicId, propertyPublicId, tenantId, tenantAssignmentPublicId);
            return Ok(propertyTenantAssignments);
        }

        
        [HttpGet("search-tenant/{searchText}")]
        public async Task<IActionResult> SearchTenant(string searchText)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var tenants = await _tenantService.SearchTenantAsync(userPublicId, searchText);
            return Ok(tenants);
        }

        [HttpDelete("delete-property-tenant/{propertyPublicId}/{tenantId}")]
        public async Task<IActionResult> DeletePropertyTenant(Guid propertyPublicId, long tenantId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _tenantService.DeletePropertyTenantAsync(userPublicId, propertyPublicId, tenantId);
            return Ok(result);
        }

        [HttpDelete("delete-tenant-assignment/{propertyPublicId}/{tenantAssignmentPublicId}")]
        public async Task<IActionResult> DeleteTenantAssignment(Guid propertyPublicId, Guid tenantAssignmentPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var result = await _tenantService.DeleteTenantAssignmentAsync(userPublicId, propertyPublicId, tenantAssignmentPublicId);
            return Ok(result);
        }
    }
}
