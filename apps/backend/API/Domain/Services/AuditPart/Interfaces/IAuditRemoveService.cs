using API.Common.Models.Results;

namespace API.Domain.Services.AuditPart.Interfaces
{
    public interface IAuditRemoveService
    {
        Task<Result> RemoveAuditAsync(Guid auditUuid);
    }
}
