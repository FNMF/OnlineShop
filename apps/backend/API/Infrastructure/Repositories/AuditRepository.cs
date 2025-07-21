using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class AuditRepository:IAuditRepository
    {
        private readonly OnlineshopContext _context;
        public AuditRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Audit> QueryAudits()
        {
            return _context.Audits;
        }
        public async Task<bool> AddAuditAsync(Audit audit)
        {
            await _context.Audits.AddAsync(audit);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddBatchAuditAsync(List<Audit> audits)
        {
            await _context.Audits.AddRangeAsync(audits);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAuditAsync(Audit audit)
        {
            _context.Audits.Update(audit);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAuditAsync(Audit audit)
        {
            _context.Audits.Remove(audit);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
