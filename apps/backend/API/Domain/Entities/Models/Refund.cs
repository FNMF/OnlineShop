using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("refund")]
[Index("RefundOrderuuid", Name = "refund_order_order_uuid_fk")]
[Index("RefundUseruuid", Name = "refund_user_user_uuid_fk")]
public partial class Refund
{
    [Key]
    [Column("refund_uuid")]
    [MaxLength(16)]
    public Guid RefundUuid { get; set; } 

    [Column("refund_useruuid")]
    [MaxLength(16)]
    public Guid RefundUseruuid { get; set; } 

    [Column("refund_orderuuid")]
    [MaxLength(16)]
    public Guid RefundOrderuuid { get; set; } 

    [Column("refund_type", TypeName = "enum('returnof','refund','discount')")]
    public string RefundType { get; set; } 

    [Column("refund_reason")]
    [StringLength(300)]
    public string? RefundReason { get; set; }

    [Column("refund_status", TypeName = "enum('create','review','pass','refuse')")]
    public string RefundStatus { get; set; } 

    [Column("refund_amount")]
    [Precision(8, 2)]
    public decimal RefundAmount { get; set; }

    [Column("refund_usercredit")]
    public int RefundUsercredit { get; set; }

    [Column("refund_time", TypeName = "datetime")]
    public DateTime RefundTime { get; set; }

    [Column("refund_isdeleted")]
    public bool RefundIsdeleted { get; set; }

    [ForeignKey("RefundOrderuuid")]
    [InverseProperty("Refunds")]
    public virtual Order RefundOrderuu { get; set; } 

    [ForeignKey("RefundUseruuid")]
    [InverseProperty("Refunds")]
    public virtual User RefundUseruu { get; set; } 
}
