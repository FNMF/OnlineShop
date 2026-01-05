using API.Application.RegionCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.RegionCase
{
    [ApiController]
    [Route("api/regions")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _service;

        public RegionController(IRegionService service)
        {
            _service = service;
        }

        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            return Ok(await _service.GetProvincesAsync());
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            return Ok(await _service.GetCitiesAsync());
        }

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts()
        {
            return Ok(await _service.GetDistrictsAsync());
        }
    }
}
