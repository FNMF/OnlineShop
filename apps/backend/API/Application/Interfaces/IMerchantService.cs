using API.Domain.Entities.Dto;
using API.Domain.Entities.Models;

namespace API.Services
{
    public interface IMerchantService
    {
        Task<List<Merchant>> GetMerchantsByLocationAsync(string province, string city);
        Task<List<Merchant>> GetMerchantBySearchAsync(string search);
        Task<Merchant> GetMerchantByAdminUuidAsync(Guid uuid);
        Task<Merchant> GetMerchantByUuidAsync(Guid uuid);
        Task<Merchant> CreateMerchant(CUMerchantDto dto);
        Task<Merchant> UpdateMerchant(CUMerchantDto dto, Guid uuid);
        Task<bool?> DeleteMerchant(Guid uuid);
    }
}
