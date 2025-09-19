using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("orders")]
[Index("UserUuid", Name = "order_user_user_uuid_fk")]
[Index("UserCouponUuid", Name = "order_usercoupon_up_uuid_fk")]
public partial class Order
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("user_uuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("total")]
    [Precision(8, 2)]
    public decimal Total { get; set; }

    [Column("order_status", TypeName = "enum('created','paid','accepted','prepared','shipped','completed','cancelled','rejected','exception')")]
    public string OrderStatus { get; set; } = null!;

    [Column("short_id")]
    [StringLength(10)]
    public string? ShortId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("merchant_address")]
    [StringLength(255)]
    public string MerchantAddress { get; set; } = null!;

    [Column("user_address")]
    [StringLength(255)]
    public string UserAddress { get; set; } = null!;

    [Column("user_coupon_uuid")]
    [MaxLength(16)]
    public Guid? UserCouponUuid { get; set; }

    [Column("order_rider_service", TypeName = "enum('scheduled','immediate','preorder','pickup')")]
    public string OrderRiderService { get; set; } = null!;

    [Column("rider")]
    [StringLength(255)]
    public string Rider { get; set; } = null!;

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [Column("list_cost")]
    [Precision(8, 2)]
    public decimal ListCost { get; set; }

    [Column("packing_cost")]
    [Precision(8, 2)]
    public decimal PackingCost { get; set; }

    [Column("rider_cost")]
    [Precision(8, 2)]
    public decimal RiderCost { get; set; }

    [Column("expected_time")]
    [StringLength(255)]
    public string ExpectedTime { get; set; } = null!;

    [Column("channel", TypeName = "enum('alipay','wechat','bank','instore','other')")]
    public string Channel { get; set; } = null!;

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("payment_uuid")]
    [MaxLength(16)]
    public Guid? PaymentUuid { get; set; }

    [InverseProperty("OrderUu")]
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    [InverseProperty("OrderUu")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("OrderUu")]
    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    [ForeignKey("UserCouponUuid")]
    [InverseProperty("Orders")]
    public virtual UserCoupon? UserCouponUu { get; set; }

    [ForeignKey("UserUuid")]
    [InverseProperty("Orders")]
    public virtual User UserUu { get; set; } = null!;
}
