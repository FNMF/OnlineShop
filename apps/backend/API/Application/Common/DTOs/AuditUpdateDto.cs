using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class AuditUpdateDto
    {
        public AuditStatus AuditStatus { get; }
        public string? AuditReason { get; }
        public AuditUpdateDto(AuditStatus auditStatus, string auditReason)
        {
            AuditStatus = auditStatus;
            AuditReason = auditReason;
        }
    }
}
