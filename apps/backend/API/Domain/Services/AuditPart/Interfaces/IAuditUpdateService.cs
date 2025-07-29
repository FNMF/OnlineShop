using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AuditPart.Interfaces
{
    public interface IAuditUpdateService
    {
        Task<Result<Audit>> MarkAsApprovalAsync(byte[] auditUuid);
        Task<Result<Audit>> MarkAsRejectionAsync(byte[] auditUuid, string reason);
    }
}
