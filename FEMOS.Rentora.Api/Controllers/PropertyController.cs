using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/property")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// GET /api/property/my-properties
        /// Returns all properties associated with the authenticated user.
        /// Response: [{ propertyId, propertyName, propertyType, city, state, addressLine1,
        ///              totalUnits, occupiedUnits, vacantUnits, roleId, roleName }]
        /// </summary>
        [HttpGet("my-properties")]
        public async Task<IActionResult> GetMyProperties()
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var properties = await _propertyService.GetMyPropertiesAsync(userPublicId);
            return Ok(properties);
        }

        /// <summary>
        /// POST /api/property/save-property
        /// Creates or updates a property. Pass PropertyId to update an existing record.
        /// Response: { status, message, propertyId }
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> SaveProperty([FromBody] UserPropertyRequestInfo objRequestInfo)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _propertyService.SavePropertyAsync(objRequestInfo);

            if (result.Status == "Failure")
                return BadRequest(result);

            return Ok(result);
        }
    }
}
