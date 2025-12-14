using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly OnlineshopContext _context;
        public RefreshTokenRepository(OnlineshopContext context) { _context = context; }
        public IQueryable<Domain.Entities.Models.RefreshToken> QueryRefreshTokens()
        {
            return _context.RefreshTokens;
        }
        public async Task<bool> AddRefreshTokenAsync(Domain.Entities.Models.RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateRefreshTokenAsync(Domain.Entities.Models.RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
