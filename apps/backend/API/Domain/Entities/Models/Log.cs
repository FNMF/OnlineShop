using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("log")]
public partial class Log
{
    [Key]
    [Column("log_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("log_type", TypeName = "enum('bp','credit','order','refund','user','product','admin','file','merchant','coupon')")]
    public string LogType { get; set; } = null!;

    [Column("log_objectuuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [Column("log_description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("log_datajson")]
    [StringLength(255)]
    public string? DataJson { get; set; }

    [Column("log_time", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("log_detail")]
    [StringLength(255)]
    public string Detail { get; set; } = null!;
}
