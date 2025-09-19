using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("auditgroups")]
public partial class Auditgroup
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("submitter_uuid")]
    [MaxLength(16)]
    public Guid SubmitterUuid { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("is_single")]
    public bool IsSingle { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("GroupUu")]
    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
