using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("refund")]
[Index("OrderUuid", Name = "refund_order_order_uuid_fk")]
[Index("Uuid", Name = "refund_user_user_uuid_fk")]
public partial class Refund
{
    [Key]
    [Column("refund_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("refund_useruuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("refund_orderuuid")]
    [MaxLength(16)]
    public Guid OrderUuid { get; set; }

    [Column("refund_type", TypeName = "enum('returnof','refund','discount')")]
    public string RefundType { get; set; } = null!;

    [Column("refund_reason")]
    [StringLength(300)]
    public string? Reason { get; set; }

    [Column("refund_status", TypeName = "enum('create','review','pass','refuse')")]
    public string RefundStatus { get; set; } = null!;

    [Column("refund_amount")]
    [Precision(8, 2)]
    public decimal Amount { get; set; }

    [Column("refund_usercredit")]
    public int UpdateUserCredit { get; set; }

    [Column("refund_time", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("refund_isdeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("OrderUuid")]
    [InverseProperty("Refunds")]
    public virtual Order RefundOrderuu { get; set; } = null!;

    [ForeignKey("Uuid")]
    [InverseProperty("Refunds")]
    public virtual User RefundUseruu { get; set; } = null!;
}
