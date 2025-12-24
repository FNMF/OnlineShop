using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantProfileService
    {
        Task<Result<AuthResult>> GetShopAdminProfile();
    }
}
