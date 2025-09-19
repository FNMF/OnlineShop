using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("order")]
[Index("Uuid", Name = "order_user_user_uuid_fk")]
[Index("UserCouponUuid", Name = "order_usercoupon_up_uuid_fk")]
public partial class Order
{
    [Key]
    [Column("order_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("order_useruuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("order_total")]
    [Precision(8, 2)]
    public decimal Total { get; set; }

    [Column("order_status", TypeName = "enum('created','paid','accepted','prepared','shipped','completed','cancelled','rejected','exception')")]
    public string OrderStatus { get; set; } = null!;

    [Column("order_sid")]
    [StringLength(10)]
    public string? ShortId { get; set; }

    [Column("order_time", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("order_ma")]
    [StringLength(255)]
    public string MerchantAddress { get; set; } = null!;

    [Column("order_ua")]
    [StringLength(255)]
    public string UserAddress { get; set; } = null!;

    [Column("order_ucuuid")]
    [MaxLength(16)]
    public Guid? UserCouponUuid { get; set; }

    [Column("order_riderservice", TypeName = "enum('scheduled','immediate','preorder','pickup')")]
    public string OrderRiderService { get; set; } = null!;

    [Column("order_rider")]
    [StringLength(255)]
    public string Rider { get; set; } = null!;

    [Column("order_note", TypeName = "text")]
    public string? Note { get; set; }

    [Column("order_cost")]
    [Precision(8, 2)]
    public decimal ListCost { get; set; }

    [Column("order_packingcharge")]
    [Precision(8, 2)]
    public decimal PackingCost { get; set; }

    [Column("order_ridercost")]
    [Precision(8, 2)]
    public decimal RiderCost { get; set; }

    [Column("order_expectedtime")]
    [StringLength(255)]
    public string ExpectedTime { get; set; } = null!;

    [Column("order_channel", TypeName = "enum('alipay','wechat','bank','instore','other')")]
    public string Channel { get; set; } = null!;

    [Column("order_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("order_paymentuuid")]
    [MaxLength(16)]
    public Guid? PaymentUuid { get; set; }

    [ForeignKey("UserCouponUuid")]
    [InverseProperty("Orders")]
    public virtual UserCoupon? OrderUcuu { get; set; }

    [ForeignKey("Uuid")]
    [InverseProperty("Orders")]
    public virtual User OrderUseruu { get; set; } = null!;

    [InverseProperty("OrderitemOrderuu")]
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    [InverseProperty("PaymentOrderuu")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("RefundOrderuu")]
    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
