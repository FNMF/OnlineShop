using API.Api.MerchantCase.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantRegisterService
    {
        Task<Result<AdminReadDto>> RegisterByPhoneAsync(MerchantRegisterByPhoneOptions opt);

    }
}
