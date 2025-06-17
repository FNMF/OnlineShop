using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IMerchantRepository
    {
        Task<List<Merchant>> GetMerchantsByLocation(string province, string city);
        Task<List<Merchant>> FuzzySearchMerchants(string search);
        Task<Merchant> GetMerchantByAdminUuid(byte[] uuidBytes);
        Task<Merchant> GetMerchantByUuid(byte[] uuidBytes);
        Task<Merchant> AddMerchant(Merchant merchant);
        Task<Merchant> UpdateMerchant(Merchant merchant);
    }
}
