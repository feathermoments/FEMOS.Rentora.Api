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
    }
}
