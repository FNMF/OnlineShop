using API.Api.MerchantCase.Models;
using API.Application.MerchantCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantManagementService
    {
        Task<Result<AdminMerchantResult>> CreateMerchantAsync(MerchantCreateOptions opt);
        Task<Result<AdminMerchantResult>> GetMerchantDetailAsync();
        Task<Result<UserMerchantResult>> GetMerchantAsync(Guid uuid);
        Task<Result<AdminMerchantResult>> UpdateMerchantAsync(MerchantUpdateOptions opt);
    }
}
