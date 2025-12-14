using API.Api.MerchantCase.Models;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantLoginService
    {
        Task<Result> LoginByTokenAsync(string accessToken, string refreshToken);
        Task<Result> LoginByAccountAsync(MerchantLoginByAccountOptions loginDto);
    }
}
