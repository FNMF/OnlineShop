using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("admin")]
[Index("Account", Name = "admin_pk", IsUnique = true)]
[Index("Phone", Name = "admin_pk2", IsUnique = true)]
public partial class Admin
{
    [Key]
    [Column("admin_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("admin_account")]
    public int Account { get; set; }

    [Column("admin_phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [Column("admin_salt")]
    [StringLength(255)]
    public string Salt { get; set; } = null!;

    [Column("admin_pwdhash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("admin_key")]
    [StringLength(10)]
    public string Key { get; set; } = null!;

    [Column("admin_lastlocation")]
    [StringLength(255)]
    public string LastLocation { get; set; } = null!;

    [Column("admin_lastlogintime", TypeName = "datetime")]
    public DateTime LastLoginTime { get; set; }

    [Column("admin_isdeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("ArAdminuu")]
    public virtual ICollection<AdminRole> AdminRoles { get; set; } = new List<AdminRole>();

    [InverseProperty("MerchantAdminuu")]
    public virtual ICollection<Merchant> Merchants { get; set; } = new List<Merchant>();
}
