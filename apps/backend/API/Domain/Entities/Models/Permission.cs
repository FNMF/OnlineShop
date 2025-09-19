using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("permission")]
public partial class Permission
{
    [Key]
    [Column("permission_id")]
    public int Id { get; set; }

    [Column("permission_name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("permission_displayname")]
    [StringLength(50)]
    public string DisplayName { get; set; } = null!;

    [Column("permission_group", TypeName = "enum('user','product','order','shop','warehouse','marketing','support','finance','system','other')")]
    public string PermissionGroup { get; set; } = null!;

    [InverseProperty("RpPermission")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
