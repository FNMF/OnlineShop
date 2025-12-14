using API.Api.IdentityCase.Models;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IMerchantLoginService
    {
        Task<Result> LoginByTokenAsync(string accessToken, string refreshToken);
        Task<Result> LoginByAccountAsync(MerchantLoginByAccountOptions loginDto);
    }
}
