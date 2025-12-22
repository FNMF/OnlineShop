using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Domain.Services.IdentityPart.Interfaces
{
    public interface IAdminPasswordVerifyService
    {
        Task<Result> VerifyPlatformPasswordAsync(int account, string password);
        Task<Result<AdminReadDto>> VerifyShopPasswordAsync(int account, string password);
    }
}
