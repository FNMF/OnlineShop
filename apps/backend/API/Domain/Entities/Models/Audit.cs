using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("audit")]
[Index("AuditGroupuuid", Name = "audit_auditgroup_ag_uuid_fk")]
public partial class Audit
{
    [Key]
    [Column("audit_uuid")]
    [MaxLength(16)]
    public Guid AuditUuid { get; set; }

    [Column("audit_objectuuid")]
    [MaxLength(16)]
    public Guid AuditObjectuuid { get; set; }

    [Column("audit_type", TypeName = "enum('store','product','banner','comment','promotion')")]
    public string AuditType { get; set; } = null!;

    [Column("audit_submitteruuid")]
    [MaxLength(16)]
    public Guid AuditSubmitteruuid { get; set; }

    [Column("audit_submitertype", TypeName = "enum('merchant','user')")]
    public string AuditSubmitertype { get; set; } = null!;

    [Column("audit_status", TypeName = "enum('pending','approval','rejection')")]
    public string AuditStatus { get; set; } = null!;

    [Column("audit_reason")]
    [StringLength(255)]
    public string AuditReason { get; set; } = null!;

    [Column("audit_auditoruuid")]
    [MaxLength(16)]
    public Guid AuditAuditoruuid { get; set; }

    [Column("audit_createdat", TypeName = "datetime")]
    public DateTime AuditCreatedat { get; set; }

    [Column("audit_reviewedat", TypeName = "datetime")]
    public DateTime AuditReviewedat { get; set; }

    [Column("audit_groupuuid")]
    [MaxLength(16)]
    public Guid AuditGroupuuid { get; set; }

    [Column("audit_isdeleted")]
    public bool AuditIsdeleted { get; set; }

    [ForeignKey("AuditGroupuuid")]
    [InverseProperty("Audits")]
    public virtual Auditgroup AuditGroupuu { get; set; } = null!;
}
