using API.Api.MerchantCase.Models;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantLoginService
    {
        Task<Result> LoginByAccountAsync(MerchantLoginByAccountDto loginDto);
    }
}
