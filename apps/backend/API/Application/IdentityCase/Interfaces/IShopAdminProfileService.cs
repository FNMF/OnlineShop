using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IShopAdminProfileService
    {
        Task<Result<AuthResult>> GetShopAdminProfile();
    }
}
