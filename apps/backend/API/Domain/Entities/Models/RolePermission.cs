using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("role_permission")]
[Index("RpPermissionid", Name = "role_permission_permission_permission_id_fk")]
[Index("RpRoleid", Name = "role_permission_role_role_id_fk")]
public partial class RolePermission
{
    [Key]
    [Column("rp_id")]
    public int RpId { get; set; }

    [Column("rp_roleid")]
    public int RpRoleid { get; set; }

    [Column("rp_permissionid")]
    public int RpPermissionid { get; set; }

    [ForeignKey("RpPermissionid")]
    [InverseProperty("RolePermissions")]
    public virtual Permission RpPermission { get; set; } 

    [ForeignKey("RpRoleid")]
    [InverseProperty("RolePermissions")]
    public virtual Role RpRole { get; set; } 
}
