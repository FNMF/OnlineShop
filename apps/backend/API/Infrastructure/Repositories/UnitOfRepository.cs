using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class UnitOfRepository: IUnitOfRepository
    {
        private readonly OnlineshopContext _context;
        public UnitOfRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
