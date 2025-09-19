using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [Column("is_build_in")]
    public bool IsBuildIn { get; set; }

    [Column("display_name")]
    [StringLength(20)]
    public string DisplayName { get; set; } = null!;

    [Column("role_type", TypeName = "enum('system','platform','shop')")]
    public string RoleType { get; set; } = null!;

    [InverseProperty("ArRole")]
    public virtual ICollection<AdminRole> AdminRoles { get; set; } = new List<AdminRole>();

    [InverseProperty("RpRole")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
