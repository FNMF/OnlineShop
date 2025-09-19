using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("refunds")]
[Index("OrderUuid", Name = "refund_order_order_uuid_fk")]
[Index("UserUuid", Name = "refund_user_user_uuid_fk")]
public partial class Refund
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("user_uuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("order_uuid")]
    [MaxLength(16)]
    public Guid OrderUuid { get; set; }

    [Column("refund_type", TypeName = "enum('returnof','refund','discount')")]
    public string RefundType { get; set; } = null!;

    [Column("reason")]
    [StringLength(300)]
    public string? Reason { get; set; }

    [Column("refund_status", TypeName = "enum('create','review','pass','refuse')")]
    public string RefundStatus { get; set; } = null!;

    [Column("amount")]
    [Precision(8, 2)]
    public decimal Amount { get; set; }

    [Column("update_user_credit")]
    public int UpdateUserCredit { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("OrderUuid")]
    [InverseProperty("Refunds")]
    public virtual Order OrderUu { get; set; } = null!;

    [ForeignKey("UserUuid")]
    [InverseProperty("Refunds")]
    public virtual User UserUu { get; set; } = null!;
}
