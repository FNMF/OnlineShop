using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("privileges")]
public partial class Privilege
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("display_name")]
    [StringLength(50)]
    public string DisplayName { get; set; } = null!;

    [InverseProperty("UpPrivilege")]
    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();
}
