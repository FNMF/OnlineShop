using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IAuditRepository
    {
        IQueryable<Audit> QueryAudits();
        Task<bool> AddAuditAsync(Audit audit);
        Task<bool> AddBatchAuditAsync(List<Audit> audits);
        Task<bool> UpdateAuditAsync(Audit audit);
        Task<bool> DeleteAuditAsync(Audit audit);
        Task SaveChangesAsync();
    }
}
