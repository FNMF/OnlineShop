using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("users")]
public partial class User
{
    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("open_id")]
    [StringLength(28)]
    public string OpenId { get; set; } = null!;

    [Column("bonus_point")]
    public int BonusPoint { get; set; }

    [Column("credit")]
    public int Credit { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [InverseProperty("UserUu")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("UserUu")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("UserUu")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("UserUu")]
    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    [InverseProperty("UserUu")]
    public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();

    [InverseProperty("UpUseruu")]
    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();
}
