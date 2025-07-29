using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly OnlineshopContext _context;

        public MerchantRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Merchant> QueryMerchants()
        {
            return _context.Merchants;
        }
        public async Task<bool> AddMerchantAsync(Merchant merchant)
        {
            await _context.Merchants.AddAsync(merchant);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateMerchantAsync(Merchant merchant)
        {
            _context.Merchants.Update(merchant);
            await _context.SaveChangesAsync();
            return true;
        }
        /*
        public async Task<List<Merchant>> GetMerchantsByLocation(string province, string city)
        {
            return await _context.Merchants
                .Where(m => m.MerchantProvince == province && m.MerchantCity == city)
                .ToListAsync();
        }
        public async Task<List<Merchant>> FuzzySearchMerchants(string search)
        {
            return await _context.Merchants
                .Where(m => m.MerchantName.Contains(search) || m.MerchantDetail.Contains(search))
                .ToListAsync();
        }
        public async Task<Merchant> GetMerchantByAdminUuid(byte[] uuidBytes)
        {
            return await _context.Merchants
                .FirstOrDefaultAsync(m => m.MerchantAdminuuid == uuidBytes);
        }
        public async Task<Merchant> GetMerchantByUuid(byte[] uuidBytes)
        {
            return await _context.Merchants
                .FirstOrDefaultAsync(m => m.MerchantUuid == uuidBytes);
        }
        public async Task<Merchant> AddMerchant(Merchant merchant)
        {
            await _context.Merchants.AddAsync(merchant);
            await _context.SaveChangesAsync();
            return merchant;
        }
        public async Task<Merchant> UpdateMerchant(Merchant merchant)
        {
            _context.Merchants.Update(merchant);
            await _context.SaveChangesAsync();
            return merchant;
        }
        */

    }
}
