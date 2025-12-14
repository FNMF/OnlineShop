using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        IQueryable<RefreshToken> QueryRefreshTokens();
        Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
