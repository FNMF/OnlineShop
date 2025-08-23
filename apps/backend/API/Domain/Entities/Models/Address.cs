using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("address")]
[Index("AddressUseruuid", Name = "address_user_user_uuid_fk")]
public partial class Address
{
    [Key]
    [Column("address_uuid")]
    [MaxLength(16)]
    public Guid AddressUuid { get; set; }

    [Column("address_useruuid")]
    [MaxLength(16)]
    public Guid AddressUseruuid { get; set; }

    [Column("address_name")]
    [StringLength(50)]
    public string AddressName { get; set; }

    [Column("address_phone")]
    [StringLength(255)]
    public string AddressPhone { get; set; }

    [Column("address_province")]
    [StringLength(50)]
    public string AddressProvince { get; set; }

    [Column("address_city")]
    [StringLength(50)]
    public string AddressCity { get; set; }

    [Column("address_district")]
    [StringLength(50)]
    public string AddressDistrict { get; set; }

    [Column("address_detail")]
    [StringLength(255)]
    public string AddressDetail { get; set; }

    [Column("address_time", TypeName = "datetime")]
    public DateTime AddressTime { get; set; }

    [Column("address_isdeleted")]
    public bool AddressIsdeleted { get; set; }

    [ForeignKey("AddressUseruuid")]
    [InverseProperty("Addresses")]
    public virtual User AddressUseruu { get; set; } = null!;
}
