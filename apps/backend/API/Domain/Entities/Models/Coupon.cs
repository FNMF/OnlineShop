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
    public int CouponId { get; set; }

    [Column("coupon_title")]
    [StringLength(50)]
    public string CouponTitle { get; set; } 

    [Column("coupon_type", TypeName = "enum('FM','discount')")]
    public string CouponType { get; set; } 

    [Column("coupon_value")]
    [Precision(5, 2)]
    public decimal CouponValue { get; set; }

    [Column("coupon_minspent")]
    [Precision(8, 2)]
    public decimal CouponMinspent { get; set; }

    [Column("coupon_startdate", TypeName = "datetime")]
    public DateTime CouponStartdate { get; set; }

    [Column("coupon_enddate", TypeName = "datetime")]
    public DateTime CouponEnddate { get; set; }

    [Column("coupon_totalcount")]
    public int CouponTotalcount { get; set; }

    [Column("coupon_limit")]
    public int CouponLimit { get; set; }

    [Column("coupon_status", TypeName = "enum('NA','A','OT')")]
    public string CouponStatus { get; set; } 

    [Column("coupon_isdeleted")]
    public bool CouponIsdeleted { get; set; }

    [Column("coupon_isaudited")]
    public bool CouponIsaudited { get; set; }

    [InverseProperty("UcCoupon")]
    public virtual ICollection<Usercoupon> Usercoupons { get; set; } = new List<Usercoupon>();
}
