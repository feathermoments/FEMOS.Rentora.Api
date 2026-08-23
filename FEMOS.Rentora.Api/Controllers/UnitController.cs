using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        public UnitController(IUnitService unitService) 
        {
            _unitService = unitService;
        }

        [HttpGet("property-units/{propertyPublicId}")]
        public async Task<IActionResult> GetPropertyUnits(Guid propertyPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var properties = await _unitService.GetPropertyUnitsAsync(userPublicId, propertyPublicId);
            return Ok(properties);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SavePropertyUnit([FromBody] PropertyUnitRequestInfo objRequestInfo)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _unitService.SavePropertyUnitAsync(objRequestInfo);

            if (result.Status == "Failure")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("details/{propertyPublicId}/{unitId}")]
        public async Task<IActionResult> GetPropertyUnitDetails(Guid propertyPublicId, long unitId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var propertyUnitDetails = await _unitService.GetPropertyUnitDetailsAsync(userPublicId, propertyPublicId, unitId);
            return Ok(propertyUnitDetails);
        }

        [HttpGet("getVacantUnits/{propertyPublicId}")]
        public async Task<IActionResult> GetVacantUnits(Guid propertyPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var vacantUnits = await _unitService.GetVacantUnitsAsync(userPublicId, propertyPublicId);
            return Ok(vacantUnits);
        }
    }
}
