using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("usercoupon")]
[Index("UcCouponid", Name = "usercoupon_coupon_coupon_id_fk")]
[Index("UcUseruuid", Name = "usercoupon_user_user_uuid_fk")]
public partial class Usercoupon
{
    [Key]
    [Column("uc_uuid")]
    [MaxLength(16)]
    public byte[] UcUuid { get; set; } = null!;

    [Column("uc_couponid")]
    public int UcCouponid { get; set; }

    [Column("uc_useruuid")]
    [MaxLength(16)]
    public byte[] UcUseruuid { get; set; } = null!;

    [Column("uc_receivetime", TypeName = "datetime")]
    public DateTime UcReceivetime { get; set; }

    [Column("uc_usedtime", TypeName = "datetime")]
    public DateTime UcUsedtime { get; set; }

    [Column("uc_status", TypeName = "enum('unused','used','OT','invalidity')")]
    public string UcStatus { get; set; } = null!;

    [Column("uc_discountvalue")]
    [Precision(8, 2)]
    public decimal? UcDiscountvalue { get; set; }

    [Column("uc_isdeleted")]
    public bool UcIsdeleted { get; set; }

    [InverseProperty("OrderUcuu")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UcCouponid")]
    [InverseProperty("Usercoupons")]
    public virtual Coupon UcCoupon { get; set; } = null!;

    [ForeignKey("UcUseruuid")]
    [InverseProperty("Usercoupons")]
    public virtual User UcUseruu { get; set; } = null!;
}
