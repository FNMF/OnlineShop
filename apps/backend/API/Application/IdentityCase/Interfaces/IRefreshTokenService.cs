using API.Application.IdentityCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.IdentityCase.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<Result<RefreshTokenDto>> RefreshTokenAsync(string refreshToken);
    }
}
