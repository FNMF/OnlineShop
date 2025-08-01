﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("user")]
public partial class User
{
    [Column("user_name")]
    [StringLength(30)]
    public string UserName { get; set; } = null!;

    [Key]
    [Column("user_uuid")]
    [MaxLength(16)]
    public byte[] UserUuid { get; set; } = null!;

    [Column("user_openid")]
    [StringLength(28)]
    public string UserOpenid { get; set; } = null!;

    [Column("user_bp")]
    public int UserBp { get; set; }

    [Column("user_credit")]
    public int UserCredit { get; set; }

    [Column("user_createdat", TypeName = "datetime")]
    public DateTime UserCreatedat { get; set; }

    [Column("user_isdeleted")]
    public bool UserIsdeleted { get; set; }

    [Column("user_phone")]
    [StringLength(20)]
    public string UserPhone { get; set; } = null!;

    [InverseProperty("AddressUseruu")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("CartUseruu")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("OrderUseruu")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("RefundUseruu")]
    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    [InverseProperty("UpUseruu")]
    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();

    [InverseProperty("UcUseruu")]
    public virtual ICollection<Usercoupon> Usercoupons { get; set; } = new List<Usercoupon>();
}
