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
                .Where(m => m.Province == province && m.City == city)
                .ToListAsync();
        }
        public async Task<List<Merchant>> FuzzySearchMerchants(string search)
        {
            return await _context.Merchants
                .Where(m => m.Name.Contains(search) || m.Detail.Contains(search))
                .ToListAsync();
        }
        public async Task<Merchant> GetMerchantByAdminUuid(byte[] uuidBytes)
        {
            return await _context.Merchants
                .FirstOrDefaultAsync(m => m.AdminUuid == uuidBytes);
        }
        public async Task<Merchant> GetMerchantByUuid(byte[] uuidBytes)
        {
            return await _context.Merchants
                .FirstOrDefaultAsync(m => m.Uuid == uuidBytes);
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
