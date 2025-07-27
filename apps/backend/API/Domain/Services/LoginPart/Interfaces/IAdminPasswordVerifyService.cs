using API.Common.Models.Results;

namespace API.Domain.Services.Common.Interfaces
{
    public interface IAdminPasswordVerifyService
    {
        Task<Result> VerifyPlatformPasswordAsync(int account, string password);
        Task<Result> VerifyShopPasswordAsync(int account, string password);
    }
}
