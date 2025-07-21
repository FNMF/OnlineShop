using API.Common.Models.Results;

namespace API.Domain.Services.Common.Interfaces
{
    public interface IAdminPasswordVerifyService
    {
        Task<Result> VerifyPasswordAsync(int account, string password);
    }
}
