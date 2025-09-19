using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("addresses")]
[Index("UserUuid", Name = "address_user_user_uuid_fk")]
public partial class Address
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("user_uuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("phone")]
    [StringLength(255)]
    public string Phone { get; set; } = null!;

    [Column("province")]
    [StringLength(50)]
    public string Province { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("district")]
    [StringLength(50)]
    public string District { get; set; } = null!;

    [Column("detail")]
    [StringLength(255)]
    public string Detail { get; set; } = null!;

    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("UserUuid")]
    [InverseProperty("Addresses")]
    public virtual User UserUu { get; set; } = null!;
}
