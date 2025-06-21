using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("audit")]
public partial class Audit
{
    [Key]
    [Column("audit_uuid")]
    [MaxLength(16)]
    public byte[] AuditUuid { get; set; } = null!;

    [Column("audit_objectuuid")]
    [MaxLength(16)]
    public byte[] AuditObjectuuid { get; set; } = null!;

    [Column("audit_type", TypeName = "enum('store','product','banner','comment','promotion')")]
    public string AuditType { get; set; } = null!;

    [Column("audit_submitteruuid")]
    [MaxLength(16)]
    public byte[] AuditSubmitteruuid { get; set; } = null!;

    [Column("audit_submitertype", TypeName = "enum('merchant','user')")]
    public string AuditSubmitertype { get; set; } = null!;

    [Column("audit_status", TypeName = "enum('pending','approval','rejection')")]
    public string AuditStatus { get; set; } = null!;

    [Column("audit_reason")]
    [StringLength(255)]
    public string AuditReason { get; set; } = null!;

    [Column("audit_auditoruuid")]
    [MaxLength(16)]
    public byte[] AuditAuditoruuid { get; set; } = null!;

    [Column("audit_createdat", TypeName = "datetime")]
    public DateTime AuditCreatedat { get; set; }

    [Column("audit_reviewedat", TypeName = "datetime")]
    public DateTime AuditReviewedat { get; set; }
}
