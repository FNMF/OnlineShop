using API.Api.IdentityCase.Models;
using API.Application.Common.DTOs;
using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IShopAdminRegisterService
    {
        Task<Result<AuthResult>> RegisterByPhoneAsync(ShopAdminRegisterByPhoneOptions opt);
        Task<Result<AuthResult>> RegisterByTempAsync(ShopAdminRegisterByTempOptions opt);
    }
}
