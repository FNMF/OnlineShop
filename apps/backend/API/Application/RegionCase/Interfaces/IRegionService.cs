using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Infrastructure.Region;

namespace API.Application.RegionCase.Interfaces
{
    public interface IRegionService
    {
        Task<Result<List<ProvinceDto>>> GetProvincesAsync();
        Task<Result<List<CityDto>>> GetCitiesAsync();
        Task<Result<List<DistrictDto>>> GetDistrictsAsync();
    }
}
