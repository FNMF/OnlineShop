using API.Entities.Models;

namespace API.Repositories
{
    public interface IMerchantRepository
    {
        Task<List<Merchant>> GetMerchantsByLocation(string province, string city);
        Task<List<Merchant>> FuzzySearchMerchants(string search);
        Task<Merchant> GetMerchantByAdminUuid(byte[] uuidBytes);
        Task<Merchant> GetMerchantByUuid(byte[] uuidBytes);
        Task<Merchant> CreateMerchant(Merchant merchant);
        Task<Merchant> UpdateMerchant(Merchant merchant);
    }
}
