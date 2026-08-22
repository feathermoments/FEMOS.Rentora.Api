using FEMOS.Rentora.Api.Authorization;
using FEMOS.Rentora.Application.Authorization;
using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Shared.Utilities;
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
        private readonly IPropertyAuthorizationContext _authContext;

        public PropertyController(
            IPropertyService propertyService,
            IPropertyAuthorizationContext authContext)
        {
            _propertyService = propertyService;
            _authContext = authContext;
        }

        /// <summary>
        /// GET /api/property/my-properties
        /// Returns all properties associated with the authenticated user.
        /// Requires: PROPERTY.VIEW permission
        /// 
        /// Response: [{ propertyId, propertyName, propertyType, city, state, addressLine1,
        ///              totalUnits, occupiedUnits, vacantUnits, roleId, roleName }]
        /// </summary>
        [HttpGet("my-properties")]
        [RequirePermission("MY.PROPERTIES", requirePropertyContext: false)]
        public async Task<IActionResult> GetMyProperties()
        {
            // For global endpoints that don't require property context,
            // we just check if user has the permission across any of their properties
            var userPublicId = User.GetUserPublicId();

            var properties = await _propertyService.GetMyPropertiesAsync(userPublicId);
            return Ok(properties);
        }

        /// <summary>
        /// POST /api/property/save-property
        /// Creates or updates a property. Pass PropertyId to update an existing record.
        /// Requires: X-Property-Public-Id header and PROPERTY.EDIT permission
        /// 
        /// Response: { status, message, propertyId }
        /// </summary>
        [HttpPost("save")]
        [RequirePermission("PROPERTY.EDIT", requirePropertyContext: true)]
        public async Task<IActionResult> SaveProperty([FromBody] UserPropertyRequestInfo objRequestInfo)
        {
            // Verify property context is valid
            if (!_authContext.IsValid)
                return Forbid("Property context is required.");

            // Verify user has permission
            if (!_authContext.HasPermission("PROPERTY.EDIT"))
                return Forbid("You do not have permission to edit properties.");

            var userPublicId = User.GetUserPublicId();
            objRequestInfo.UserPublicId = userPublicId;

            var result = await _propertyService.SavePropertyAsync(objRequestInfo);

            if (result.Status == "Failure")
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// GET /api/property/details/{propertyId}
        /// Returns property details associated with the authenticated user.
        /// Requires: X-Property-Public-Id header and PROPERTY.VIEW permission
        /// 
        /// Response: { propertyId, propertyName, propertyType, city, state, addressLine1,
        ///              totalUnits, occupiedUnits, vacantUnits, roleId, roleName }
        /// </summary>
        [HttpGet("details/{propertyId}")]
        [RequirePermission("PROPERTY.VIEW", requirePropertyContext: true)]
        public async Task<IActionResult> GetPropertyDetails(long propertyId)
        {
            // Verify property context is valid
            if (!_authContext.IsValid)
                return Forbid("Property context is required.");

            // Verify user has permission
            if (!_authContext.HasPermission("PROPERTY.VIEW"))
                return Forbid("You do not have permission to view properties.");

            var userPublicId = User.GetUserPublicId();

            var propertyDetails = await _propertyService.GetPropertyDetailsAsync(userPublicId, propertyId);
            return Ok(propertyDetails);
        }
    }
}
