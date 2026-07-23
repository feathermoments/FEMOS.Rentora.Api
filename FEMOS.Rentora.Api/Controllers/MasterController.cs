using FEMOS.Rentora.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("getPropertyTypes")]
        public async Task<IActionResult> GetPropertyTypes()
        {
            var result = await _masterService.GetPropertyTypes();
            return Ok(result);
        }

        [HttpGet("getCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _masterService.GetCountries();
            return Ok(result);
        }

        [HttpGet("getStatesByCountryId/{countryId}")]
        public async Task<IActionResult> GetStatesByCountryId(int countryId)
        {
            var result = await _masterService.GetStatesByCountryId(countryId);
            return Ok(result);
        }

        [HttpGet("getCitiesByStateId/{stateId}")]
        public async Task<IActionResult> GetCitiesByStateId(int stateId)
        {
            var result = await _masterService.GetCitiesByStateId(stateId);
            return Ok(result);

        }

        [HttpGet("getUnitTypesByPropertyType/{propertyTypeId}")]
        public async Task<IActionResult> GetUnitTypesByPropertyType(int propertyTypeId)
        {
            var result = await _masterService.GetUnitTypeByPropertyTypeAsync(propertyTypeId);
            return Ok(result);
        }

        [HttpGet("getUnitTypes")]
        public async Task<IActionResult> GetUnitTypes()
        {
            var result = await _masterService.GetUnitTypesAsync();
            return Ok(result);
        }

        [HttpGet("getBHKTypes")]
        public async Task<IActionResult> GetBHKTypes()
        {
            var result = await _masterService.GetBHKTypesAsync();
            return Ok(result);
        }

        [HttpGet("getFurnishingTypes")]
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var result = await _masterService.GetFurnishingTypesAsync();
            return Ok(result);
        }

        [HttpGet("getUnitStatusTypes")]
        public async Task<IActionResult> GetUnitStatusTypes()
        {
            var result = await _masterService.GetUnitStatusTypesAsync();
            return Ok(result);
        }

        [HttpGet("getPropertyStatusTypes")]
        public async Task<IActionResult> GetPropertyStatusTypes()
        {
            var result = await _masterService.GetPropertyStatusTypesAsync();
            return Ok(result);
        }

        [HttpGet("getAgreementStatusTypes")]
        public async Task<IActionResult> GetAgreementStatusTypes()
        {
            var result = await _masterService.GetAgreementStatusTypesAsync();
            return Ok(result);
        }

        [HttpGet("getTenantAssignmentStatusTypes")]
        public async Task<IActionResult> GetTenantAssignmentStatusTypes()
        {
            var result = await _masterService.GetTenantAssignmentStatusTypesAsync();
            return Ok(result);
        }

        [HttpGet("getPaymentMethods")]
                public async Task<IActionResult> GetPaymentMethods()
        {
            var result = await _masterService.GetPaymentMethodsAsync();
            return Ok(result);
        }
    }
}
