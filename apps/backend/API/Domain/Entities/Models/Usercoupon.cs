using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("user_coupons")]
[Index("CouponId", Name = "usercoupon_coupon_coupon_id_fk")]
[Index("UserUuid", Name = "usercoupon_user_user_uuid_fk")]
public partial class UserCoupon
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("coupon_id")]
    public int CouponId { get; set; }

    [Column("user_uuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("received_at", TypeName = "datetime")]
    public DateTime ReceivedAt { get; set; }

    [Column("used_at", TypeName = "datetime")]
    public DateTime UsedAt { get; set; }

    [Column("user_coupon_status", TypeName = "enum('unused','used','OT','invalidity')")]
    public string UserCouponStatus { get; set; } = null!;

    [Column("discount_value")]
    [Precision(8, 2)]
    public decimal? DiscountValue { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("CouponId")]
    [InverseProperty("UserCoupons")]
    public virtual Coupon Coupon { get; set; } = null!;

    [InverseProperty("UserCouponUu")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserUuid")]
    [InverseProperty("UserCoupons")]
    public virtual User UserUu { get; set; } = null!;
}
