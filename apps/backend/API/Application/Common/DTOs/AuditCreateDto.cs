namespace API.Application.Common.DTOs
{
    public class AuditCreateDto
    {
        public byte[] AuditObjectuuid {get;}
        public string AuditType { get; }
        public byte[] AuditSubmitteruuid { get; }
        public string AuditSubmitertype { get; }
        public byte[] AuditAuditoruuid { get; }
        public DateTime AuditCreatedat { get; }
        public DateTime AuditReviewedat { get; }
        public byte[] AuditGroupuuid { get; }
        public AuditCreateDto(byte[] auditObjectuuid, string auditType, byte[] auditSubmitteruuid, string auditSubmitertype, byte[] auditAuditoruuid, DateTime auditCreatedat, DateTime auditReviewedat, byte[] auditGroupuuid)
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
