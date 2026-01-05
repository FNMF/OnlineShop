using API.Application.RegionCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Infrastructure.Caching;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Infrastructure.Region
{
    public class RegionService:IRegionService
    {
        private readonly IMemoryCache _cache;
        private readonly OnlineShopContext _db;

        public RegionService(
            IMemoryCache cache,
            OnlineShopContext db)
        {
            _cache = cache;
            _db = db;
        }

        public async Task<Result<List<ProvinceDto>>> GetProvincesAsync()
        {
            var provinces = await _cache.GetOrCreateAsync(
                CacheKeys.Provinces,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                    return await _db.Provinces
                        .AsNoTracking()
                        .OrderBy(p => p.Id)
                        .ToListAsync();
                });

            var provinecDtos = provinces.Select(p => new ProvinceDto { Id = p.Id, Name = p.Name }).ToList();

            return Result<List<ProvinceDto>>.Success(provinecDtos);
        }

        public async Task<Result<List<CityDto>>> GetCitiesAsync(int provinceId)
        {
            var cities= await _cache.GetOrCreateAsync(
                CacheKeys.Cities,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                    return await _db.Cities
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .ToListAsync();
                });
            var cityDtos = cities.Where(c => c.ProvinceId ==provinceId).Select(c => new CityDto { Id = c.Id, Name = c.Name, ProvinceId = c.ProvinceId!.Value }).ToList();

            return Result<List<CityDto>>.Success(cityDtos);
        }

        public async Task<Result<List<DistrictDto>>> GetDistrictsAsync(int cityId)
        {
            var districts = await _cache.GetOrCreateAsync(
                CacheKeys.Districts,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                    return await _db.Districts
                        .AsNoTracking()
                        .OrderBy(d => d.Id)
                        .ToListAsync();
                });
            var districtDtos = districts.Where(d => d.CityId == cityId).Select(d => new DistrictDto { Id = d.Id, Name = d.Name, CityId = d.CityId!.Value }).ToList();

            return Result<List<DistrictDto>>.Success(districtDtos);
        }

        public void ClearAll()
        {
            _cache.Remove(CacheKeys.Provinces);
            _cache.Remove(CacheKeys.Cities);
            _cache.Remove(CacheKeys.Districts);
        }
    }
}
