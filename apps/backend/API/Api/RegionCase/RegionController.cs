using API.Application.RegionCase.Interfaces;
using API.Common.Models.Results;
using API.Infrastructure.Region;
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
            var result = await _service.GetProvincesAsync();
            return Ok(result);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities([FromQuery] int provinceId)
        {
            var result = await _service.GetCitiesAsync(provinceId);
            return Ok(result);
        }

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts([FromQuery] int cityId)
        {
            var result = await _service.GetDistrictsAsync(cityId);
            return Ok(result);
        }
    }
}
