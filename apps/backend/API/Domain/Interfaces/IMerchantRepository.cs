using API.Domain.Entities.Models;
using API.Infrastructure.Database;

namespace API.Domain.Interfaces
{
    public interface IMerchantRepository
    {
        IQueryable<Merchant> QueryMerchants();
        Task<bool> AddMerchantAsync(Merchant merchant);
        Task<bool> UpdateMerchantAsync(Merchant merchant);
        /*
        Task<List<Merchant>> GetMerchantsByLocation(string province, string city);
        Task<List<Merchant>> FuzzySearchMerchants(string search);
        Task<Merchant> GetMerchantByAdminUuid(byte[] uuidBytes);
        Task<Merchant> GetMerchantByUuid(byte[] uuidBytes);
        Task<Merchant> AddMerchant(Merchant merchant);
        Task<Merchant> UpdateMerchant(Merchant merchant);
        */
    }
}
