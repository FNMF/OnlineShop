using API.Application.RegionCase.Interfaces;
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

        public async Task<List<Province>> GetProvincesAsync()
        {
            return await _cache.GetOrCreateAsync(
                CacheKeys.Provinces,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                    return await _db.Provinces
                        .AsNoTracking()
                        .OrderBy(p => p.Id)
                        .ToListAsync();
                });
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            return await _cache.GetOrCreateAsync(
                CacheKeys.Cities,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                    return await _db.Cities
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .ToListAsync();
                });
        }

        public async Task<List<District>> GetDistrictsAsync()
        {
            return await _cache.GetOrCreateAsync(
                CacheKeys.Districts,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                    return await _db.Districts
                        .AsNoTracking()
                        .OrderBy(d => d.Id)
                        .ToListAsync();
                });
        }

        public void ClearAll()
        {
            _cache.Remove(CacheKeys.Provinces);
            _cache.Remove(CacheKeys.Cities);
            _cache.Remove(CacheKeys.Districts);
        }
    }
}
