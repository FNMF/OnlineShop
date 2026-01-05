using API.Domain.Entities.Models;

namespace API.Application.RegionCase.Interfaces
{
    public interface IRegionService
    {
        Task<List<Province>> GetProvincesAsync();
        Task<List<City>> GetCitiesAsync();
        Task<List<District>> GetDistrictsAsync();
    }
}
