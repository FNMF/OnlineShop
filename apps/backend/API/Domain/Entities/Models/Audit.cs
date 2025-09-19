using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("audits")]
[Index("GroupUuid", Name = "audit_auditgroup_ag_uuid_fk")]
public partial class Audit
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("object_uuid")]
    [MaxLength(16)]
    public Guid ObjectUuid { get; set; }

    [Column("audit_type", TypeName = "enum('store','product','banner','comment','promotion')")]
    public string AuditType { get; set; } = null!;

    [Column("submitter_uuid")]
    [MaxLength(16)]
    public Guid SubmitterUuid { get; set; }

    [Column("submiter_type", TypeName = "enum('merchant','user')")]
    public string SubmiterType { get; set; } = null!;

    [Column("audit_status", TypeName = "enum('pending','approval','rejection')")]
    public string AuditStatus { get; set; } = null!;

    [Column("reason")]
    [StringLength(255)]
    public string Reason { get; set; } = null!;

    [Column("auditor_uuid")]
    [MaxLength(16)]
    public Guid AuditorUuid { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("reviewed_at", TypeName = "datetime")]
    public DateTime ReviewedAt { get; set; }

    [Column("group_uuid")]
    [MaxLength(16)]
    public Guid GroupUuid { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("GroupUuid")]
    [InverseProperty("Audits")]
    public virtual Auditgroup GroupUu { get; set; } = null!;
}
