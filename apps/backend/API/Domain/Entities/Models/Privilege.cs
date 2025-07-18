﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("privilege")]
public partial class Privilege
{
    [Key]
    [Column("privilege_id")]
    public int PrivilegeId { get; set; }

    [Column("privilege_name")]
    [StringLength(50)]
    public string PrivilegeName { get; set; } = null!;

    [Column("privilege_displayname")]
    [StringLength(50)]
    public string PrivilegeDisplayname { get; set; } = null!;

    [InverseProperty("UpPrivilege")]
    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();
}
