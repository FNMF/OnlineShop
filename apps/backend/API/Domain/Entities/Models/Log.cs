using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("logs")]
public partial class Log
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("log_type", TypeName = "enum('bp','credit','order','refund','user','product','admin','file','merchant','coupon')")]
    public string LogType { get; set; } = null!;

    [Column("object_uuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("data_json")]
    [StringLength(255)]
    public string? DataJson { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("detail")]
    [StringLength(255)]
    public string Detail { get; set; } = null!;
}
