using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("role")]
public partial class Role
{
    [Key]
    [Column("role_id")]
    public int Id { get; set; }

    [Column("role_name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [Column("role_isbuildin")]
    public bool IsBuildIn { get; set; }

    [Column("role_displayname")]
    [StringLength(20)]
    public string DisplayName { get; set; } = null!;

    [Column("role_type", TypeName = "enum('system','platform','shop')")]
    public string RoleType { get; set; } = null!;

    [InverseProperty("ArRole")]
    public virtual ICollection<AdminRole> AdminRoles { get; set; } = new List<AdminRole>();

    [InverseProperty("RpRole")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
