using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("admin_role")]
[Index("ArAdminuuid", Name = "admin_role_admin_admin_uuid_fk")]
[Index("ArRoleid", Name = "admin_role_role_role_id_fk")]
public partial class AdminRole
{
    [Key]
    [Column("ar_id")]
    public int ArId { get; set; }

    [Column("ar_adminuuid")]
    [MaxLength(16)]
    public byte[] ArAdminuuid { get; set; } = null!;

    [Column("ar_roleid")]
    public int ArRoleid { get; set; }

    [ForeignKey("ArAdminuuid")]
    [InverseProperty("AdminRoles")]
    public virtual Admin ArAdminuu { get; set; } = null!;

    [ForeignKey("ArRoleid")]
    [InverseProperty("AdminRoles")]
    public virtual Role ArRole { get; set; } = null!;
}
