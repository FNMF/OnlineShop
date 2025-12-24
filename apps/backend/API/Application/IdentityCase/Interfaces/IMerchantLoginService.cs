using API.Api.IdentityCase.Models;
using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IMerchantLoginService
    {
        Task<Result<AuthResult>> RefreshTokenAsync(string accessToken, string refreshToken);
        Task<Result<AuthResult>> LoginByAccountAsync(MerchantLoginByAccountOptions opt);
        Task<Result<AuthResult>> LoginByPhoneAsync(MerchantLoginByValidationCodeOptions opt);
    }
}
