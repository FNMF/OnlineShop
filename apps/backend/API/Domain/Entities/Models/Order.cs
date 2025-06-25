using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("order")]
[Index("OrderUseruuid", Name = "order_user_user_uuid_fk")]
[Index("OrderUcuuid", Name = "order_usercoupon_up_uuid_fk")]
public partial class Order
{
    [Key]
    [Column("order_uuid")]
    [MaxLength(16)]
    public byte[] OrderUuid { get; set; } = null!;

    [Column("order_useruuid")]
    [MaxLength(16)]
    public byte[] OrderUseruuid { get; set; } = null!;

    [Column("order_total")]
    [Precision(8, 2)]
    public decimal OrderTotal { get; set; }

    [Column("order_status", TypeName = "enum('created','unpaid','unaccepted','preparing','unship','shipping','done','canceled','reject','exception')")]
    public string OrderStatus { get; set; } = null!;

    [Column("order_sid")]
    [StringLength(10)]
    public string? OrderSid { get; set; }

    [Column("order_time", TypeName = "datetime")]
    public DateTime OrderTime { get; set; }

    [Column("order_ma")]
    [StringLength(255)]
    public string OrderMa { get; set; } = null!;

    [Column("order_ua")]
    [StringLength(255)]
    public string OrderUa { get; set; } = null!;

    [Column("order_ucuuid")]
    [MaxLength(16)]
    public byte[]? OrderUcuuid { get; set; }

    [Column("order_riderservice", TypeName = "enum('scheduled','immediate','preorder','pickup')")]
    public string OrderRiderservice { get; set; } = null!;

    [Column("order_rider")]
    [StringLength(255)]
    public string OrderRider { get; set; } = null!;

    [Column("order_note", TypeName = "text")]
    public string? OrderNote { get; set; }

    [Column("order_cost")]
    [Precision(8, 2)]
    public decimal OrderCost { get; set; }

    [Column("order_packingcharge")]
    [Precision(8, 2)]
    public decimal OrderPackingcharge { get; set; }

    [Column("order_ridercost")]
    [Precision(8, 2)]
    public decimal OrderRidercost { get; set; }

    [Column("order_expectedtime")]
    [StringLength(255)]
    public string OrderExpectedtime { get; set; } = null!;

    [Column("order_channel", TypeName = "enum('alipay','wechat','bank','instore','other')")]
    public string OrderChannel { get; set; } = null!;

    [Column("order_isdeleted")]
    public bool OrderIsdeleted { get; set; }

    [ForeignKey("OrderUcuuid")]
    [InverseProperty("Orders")]
    public virtual Usercoupon? OrderUcuu { get; set; }

    [ForeignKey("OrderUseruuid")]
    [InverseProperty("Orders")]
    public virtual User OrderUseruu { get; set; } = null!;

    [InverseProperty("RefundOrderuu")]
    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
