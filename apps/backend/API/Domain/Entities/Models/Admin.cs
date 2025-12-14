using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("admins")]
[Index("Account", Name = "admin_pk", IsUnique = true)]
[Index("Phone", Name = "admin_pk2", IsUnique = true)]
public partial class Admin
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("account")]
    public int Account { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [Column("salt")]
    [StringLength(255)]
    public string Salt { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("last_location")]
    [StringLength(255)]
    public string LastLocation { get; set; } = null!;

    [Column("last_login_time", TypeName = "datetime")]
    public DateTime LastLoginTime { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("ArAdminuu")]
    public virtual ICollection<AdminRole> AdminRoles { get; set; } = new List<AdminRole>();

    [InverseProperty("AdminUu")]
    public virtual ICollection<Merchant> Merchants { get; set; } = new List<Merchant>();
}
