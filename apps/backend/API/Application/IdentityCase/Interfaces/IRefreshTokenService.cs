using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<Result<String>> RefreshTokenAsync(string refreshToken);
    }
}
