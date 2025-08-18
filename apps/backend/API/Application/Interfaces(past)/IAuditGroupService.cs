using API.Domain.Entities.Models;

namespace API.Application.Interfaces
{
    public interface IAuditGroupService
    {
        Task<bool> AddAuditGroup(Auditgroup auditgroup); 
        IQueryable<Auditgroup> GetAuditGroups();        
        Task<bool> UpdateAuditGroup(Auditgroup auditgroup);
        Task<bool> RemoveAuditGroup(Auditgroup auditgroup);
        Task SaveChangesAsync();
    }
}
