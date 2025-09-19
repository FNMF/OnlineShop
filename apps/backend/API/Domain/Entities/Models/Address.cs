using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("address")]
[Index("Uuid", Name = "address_user_user_uuid_fk")]
public partial class Address
{
    [Key]
    [Column("address_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("address_useruuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("address_name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("address_phone")]
    [StringLength(255)]
    public string Phone { get; set; } = null!;

    [Column("address_province")]
    [StringLength(50)]
    public string Province { get; set; } = null!;

    [Column("address_city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("address_district")]
    [StringLength(50)]
    public string District { get; set; } = null!;

    [Column("address_detail")]
    [StringLength(255)]
    public string Detail { get; set; } = null!;

    [Column("address_time", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [Column("address_isdeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("Uuid")]
    [InverseProperty("Addresses")]
    public virtual User AddressUseruu { get; set; } = null!;
}
