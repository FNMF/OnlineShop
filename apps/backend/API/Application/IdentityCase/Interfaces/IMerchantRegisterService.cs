using API.Api.IdentityCase.Models;
using API.Application.Common.DTOs;
using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IMerchantRegisterService
    {
        Task<Result<TokenDto>> RegisterByPhoneAsync(MerchantRegisterByPhoneOptions opt);

    }
}
