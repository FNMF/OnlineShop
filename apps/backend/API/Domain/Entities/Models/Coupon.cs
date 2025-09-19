using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("coupon")]
public partial class Coupon
{
    [Key]
    [Column("coupon_id")]
    public int Id { get; set; }

    [Column("coupon_title")]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Column("coupon_type", TypeName = "enum('FM','discount')")]
    public string CouponType { get; set; } = null!;

    [Column("coupon_value")]
    [Precision(5, 2)]
    public decimal Value { get; set; }

    [Column("coupon_minspent")]
    [Precision(8, 2)]
    public decimal MinSpent { get; set; }

    [Column("coupon_startdate", TypeName = "datetime")]
    public DateTime StartTDate { get; set; }

    [Column("coupon_enddate", TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    [Column("coupon_totalcount")]
    public int TotalCount { get; set; }

    [Column("coupon_limit")]
    public int Limit { get; set; }

    [Column("coupon_status", TypeName = "enum('NA','A','OT')")]
    public string CouponStatus { get; set; } = null!;

    [Column("coupon_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("coupon_isaudited")]
    public bool IsAudited { get; set; }

    [InverseProperty("UcCoupon")]
    public virtual ICollection<UserCoupon> Usercoupons { get; set; } = new List<UserCoupon>();
}
