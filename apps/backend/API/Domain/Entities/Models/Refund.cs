using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Models;

[Table("refund")]
[Index("RefundOrderuuid", Name = "refund_order_order_uuid_fk")]
[Index("RefundUseruuid", Name = "refund_user_user_uuid_fk")]
public partial class Refund
{
    [Key]
    [Column("refund_uuid")]
    [MaxLength(16)]
    public byte[] RefundUuid { get; set; } = null!;

    [Column("refund_useruuid")]
    [MaxLength(16)]
    public byte[] RefundUseruuid { get; set; } = null!;

    [Column("refund_orderuuid")]
    [MaxLength(16)]
    public byte[] RefundOrderuuid { get; set; } = null!;

    [Column("refund_type", TypeName = "enum('return','refund','discount')")]
    public string RefundType { get; set; } = null!;

    [Column("refund_reason")]
    [StringLength(300)]
    public string? RefundReason { get; set; }

    [Column("refund_status", TypeName = "enum('new','review','pass','refuse')")]
    public string RefundStatus { get; set; } = null!;

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
    public virtual Order RefundOrderuu { get; set; } = null!;

    [ForeignKey("RefundUseruuid")]
    [InverseProperty("Refunds")]
    public virtual User RefundUseruu { get; set; } = null!;
}
