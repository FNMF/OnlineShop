using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("auditgroup")]
public partial class Auditgroup
{
    [Key]
    [Column("ag_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("ag_submitteruuid")]
    [MaxLength(16)]
    public Guid SubmitterUuid { get; set; }

    [Column("ag_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("ag_issingle")]
    public bool IsSingle { get; set; }

    [Column("ag_isdeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("AuditGroupuu")]
    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
