using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class AuditGroupRepository:IAuditGroupRepository
    {
        private readonly OnlineshopContext _context;
        public AuditGroupRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Auditgroup> QueryAuditGroups()
        {
            return _context.Auditgroups;
        }
        public async Task<bool> AddAuditGroupAsync(Auditgroup auditgroup)
        {
            await _context.Auditgroups.AddAsync(auditgroup);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAuditGroupAsync(Auditgroup auditgroup)
        {
            _context.Auditgroups.Update(auditgroup);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAuditGroupAsync(Auditgroup auditgroup)
        {
            _context.Auditgroups.Remove(auditgroup);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
