using API.Application.MerchantCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IGetMerchantService
    {
        Task<Result<UserMerchantResult>> GetMerchantAsync(Guid uuid);
        Task<Result<Guid>> GetMerchantUuid();
    }
}
