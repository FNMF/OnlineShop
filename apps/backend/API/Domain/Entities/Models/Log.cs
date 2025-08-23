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
    public Guid LogUuid { get; set; } 

    [Column("log_type", TypeName = "enum('bp','credit','order','refund','user','product','admin','file','merchant','coupon')")]
    public string LogType { get; set; } 

    [Column("log_objectuuid")]
    [MaxLength(16)]
    public Guid? LogObjectuuid { get; set; }

    [Column("log_description")]
    [StringLength(255)]
    public string LogDescription { get; set; } 

    [Column("log_datajson")]
    [StringLength(255)]
    public string? LogDatajson { get; set; }

    [Column("log_time", TypeName = "datetime")]
    public DateTime LogTime { get; set; }

    [Column("log_detail")]
    [StringLength(255)]
    public string LogDetail { get; set; } 
}
