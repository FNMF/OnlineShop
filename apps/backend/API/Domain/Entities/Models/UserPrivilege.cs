using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("user_privilege")]
[Index("UpPrivilegeid", Name = "user_privilege_privilege_privilege_id_fk")]
[Index("UpUseruuid", Name = "user_privilege_user_user_uuid_fk")]
public partial class UserPrivilege
{
    [Key]
    [Column("up_uuid")]
    [MaxLength(16)]
    public Guid UpUuid { get; set; } 

    [Column("up_useruuid")]
    [MaxLength(16)]
    public Guid UpUseruuid { get; set; } 

    [Column("up_privilegeid")]
    public int UpPrivilegeid { get; set; }

    [ForeignKey("UpPrivilegeid")]
    [InverseProperty("UserPrivileges")]
    public virtual Privilege UpPrivilege { get; set; } 

    [ForeignKey("UpUseruuid")]
    [InverseProperty("UserPrivileges")]
    public virtual User UpUseruu { get; set; } 
}
