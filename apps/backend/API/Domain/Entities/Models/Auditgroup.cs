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
    public Guid AgUuid { get; set; } 

    [Column("ag_submitteruuid")]
    [MaxLength(16)]
    public Guid AgSubmitteruuid { get; set; } 

    [Column("ag_createdat", TypeName = "datetime")]
    public DateTime AgCreatedat { get; set; }

    [Column("ag_issingle")]
    public bool AgIssingle { get; set; }

    [Column("ag_isdeleted")]
    public bool AgIsdeleted { get; set; }

    [InverseProperty("AuditGroupuu")]
    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
