using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IAuditGroupRepository
    {
        IQueryable<Auditgroup> QueryAuditGroups();
        Task<bool> AddAuditGroupAsync(Auditgroup auditgroup);
        Task<bool> UpdateAuditGroupAsync(Auditgroup auditgroup);
        Task<bool> DeleteAuditGroupAsync(Auditgroup auditgroup);
        Task SaveChangesAsync();
    }
}
