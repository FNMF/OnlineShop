using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("usercoupon")]
[Index("CouponId", Name = "usercoupon_coupon_coupon_id_fk")]
[Index("UserUuid", Name = "usercoupon_user_user_uuid_fk")]
public partial class UserCoupon
{
    [Key]
    [Column("uc_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("uc_couponid")]
    public int CouponId { get; set; }

    [Column("uc_useruuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("uc_receivetime", TypeName = "datetime")]
    public DateTime ReceivedAt { get; set; }

    [Column("uc_usedtime", TypeName = "datetime")]
    public DateTime UsedAt { get; set; }

    [Column("uc_status", TypeName = "enum('unused','used','OT','invalidity')")]
    public string UserCouponStatus { get; set; } = null!;

    [Column("uc_discountvalue")]
    [Precision(8, 2)]
    public decimal? DiscountValue { get; set; }

    [Column("uc_isdeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("OrderUcuu")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("CouponId")]
    [InverseProperty("Usercoupons")]
    public virtual Coupon UcCoupon { get; set; } = null!;

    [ForeignKey("UserUuid")]
    [InverseProperty("Usercoupons")]
    public virtual User UcUseruu { get; set; } = null!;
}
