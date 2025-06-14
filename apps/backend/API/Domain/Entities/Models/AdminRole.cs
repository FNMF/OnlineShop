using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities.Models;

[Table("admin_role")]
[Index("ArAdminuuid", Name = "admin_role_admin_admin_uuid_fk")]
[Index("ArRoleid", Name = "admin_role_role_role_id_fk")]
public partial class AdminRole
{
    [Key]
    [Column("ar_uuid")]
    [MaxLength(16)]
    public byte[] ArUuid { get; set; } = null!;

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
