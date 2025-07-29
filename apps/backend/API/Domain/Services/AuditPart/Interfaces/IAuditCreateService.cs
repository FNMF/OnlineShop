using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AuditPart.Interfaces
{
    public interface IAuditCreateService
    {
        Task<Result<Audit>> AddAuditAsync(AuditCreateDto dto);
    }
}
