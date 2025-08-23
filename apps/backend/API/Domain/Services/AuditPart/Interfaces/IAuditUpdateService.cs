using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AuditPart.Interfaces
{
    public interface IAuditUpdateService
    {
        Task<Result<Audit>> MarkAsApprovalAsync(Guid auditUuid);
        Task<Result<Audit>> MarkAsRejectionAsync(Guid auditUuid, string reason);
    }
}
