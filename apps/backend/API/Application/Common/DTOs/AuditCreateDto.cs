namespace API.Application.Common.DTOs
{
    public class AuditCreateDto
    {
        public Guid AuditObjectuuid {get;}
        public string AuditType { get; }
        public Guid AuditSubmitteruuid { get; }
        public string AuditSubmitertype { get; }
        public Guid AuditAuditoruuid { get; }
        public DateTime AuditCreatedat { get; }
        public DateTime AuditReviewedat { get; }
        public Guid AuditGroupuuid { get; }
        public AuditCreateDto(Guid auditObjectuuid, string auditType, Guid auditSubmitteruuid, string auditSubmitertype, Guid auditAuditoruuid, DateTime auditCreatedat, DateTime auditReviewedat, Guid auditGroupuuid)
        {
            AuditObjectuuid = auditObjectuuid;
            AuditType = auditType;
            AuditSubmitteruuid = auditSubmitteruuid;
            AuditSubmitertype = auditSubmitertype;
            AuditAuditoruuid = auditAuditoruuid;
            AuditCreatedat = auditCreatedat;
            AuditReviewedat = auditReviewedat;
            AuditGroupuuid = auditGroupuuid;
        }
    }
}
