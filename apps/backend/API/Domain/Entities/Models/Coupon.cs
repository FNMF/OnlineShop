using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("coupons")]
public partial class Coupon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Column("coupon_type", TypeName = "enum('FM','discount')")]
    public string CouponType { get; set; } = null!;

    [Column("value")]
    [Precision(5, 2)]
    public decimal Value { get; set; }

    [Column("min_spent")]
    [Precision(8, 2)]
    public decimal MinSpent { get; set; }

    [Column("start_date", TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column("end_date", TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    [Column("total_count")]
    public int TotalCount { get; set; }

    [Column("limit")]
    public int Limit { get; set; }

    [Column("coupon_status", TypeName = "enum('NA','A','OT')")]
    public string CouponStatus { get; set; } = null!;

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("is_audited")]
    public bool IsAudited { get; set; }

    [InverseProperty("Coupon")]
    public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
}
