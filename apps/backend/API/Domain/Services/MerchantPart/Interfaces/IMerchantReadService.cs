using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.MerchantPart.Interfaces
{
    public interface IMerchantReadService
    {
        //Task<List<Merchant>> GetMerchantsByLocationAsync(string province, string city);
        //Task<List<Merchant>> GetMerchantBySearchAsync(string search);
        //Task<Merchant> GetMerchantByAdminUuidAsync(Guid uuid);
        Task<Result<Merchant>> GetMerchantByUuidAsync(Guid uuid);
        Task<Result<Merchant>> GetMerchantByAdminUuidAsync(Guid uuid);
    }
}
