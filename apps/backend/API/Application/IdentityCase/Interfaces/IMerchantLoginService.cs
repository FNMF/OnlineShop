using API.Api.IdentityCase.Models;
using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IMerchantLoginService
    {
        Task<Result<TokenResult>> LoginByTokenAsync(string accessToken, string refreshToken);
        Task<Result<TokenResult>> LoginByAccountAsync(MerchantLoginByAccountOptions opt);
        Task<Result<TokenResult>> LoginByPhoneAsync(MerchantLoginByValidationCodeOptions opt);
    }
}
