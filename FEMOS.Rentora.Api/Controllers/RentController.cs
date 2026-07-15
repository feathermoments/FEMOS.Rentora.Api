using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
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

        //[HttpGet("rent-agreement/{propertyId}/{tenantId}")]
        //public async Task<IActionResult> GetRentAgreement(int propertyId, int tenant, int propertyUnitId)
        //{
        //    var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
        //    if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
        //        return Unauthorized();
        //    var rentAgreement = await _tenantService.GetRentAgreementAsync(userPublicId, propertyId, tenant, propertyUnitId);
        //    return Ok(rentAgreement);
        //}

    }
}
