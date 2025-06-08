using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.Models;

[Table("usercoupon")]
[Index("UpUseruuid", Name = "usercoupon_user_user_uuid_fk")]
public partial class Usercoupon
{
    [Key]
    [Column("up_uuid")]
    [MaxLength(16)]
    public byte[] UpUuid { get; set; } = null!;

    [Column("up_couponid")]
    public int UpCouponid { get; set; }

    [Column("up_useruuid")]
    [MaxLength(16)]
    public byte[] UpUseruuid { get; set; } = null!;

    [Column("up_receivetime", TypeName = "datetime")]
    public DateTime UpReceivetime { get; set; }

    [Column("up_usedtime", TypeName = "datetime")]
    public DateTime UpUsedtime { get; set; }

    [Column("up_status", TypeName = "enum('unused','used','OT','void')")]
    public string UpStatus { get; set; } = null!;

    [Column("up_discountvalue")]
    [Precision(8, 2)]
    public decimal? UpDiscountvalue { get; set; }

    [Column("up_isdeleted")]
    public bool UpIsdeleted { get; set; }

    [InverseProperty("OrderUcuu")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UpUseruuid")]
    [InverseProperty("Usercoupons")]
    public virtual User UpUseruu { get; set; } = null!;
}
