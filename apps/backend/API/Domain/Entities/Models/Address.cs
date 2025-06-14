using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Models;

[Table("address")]
[Index("AddressUseruuid", Name = "address_user_user_uuid_fk")]
public partial class Address
{
    [Key]
    [Column("address_uuid")]
    [MaxLength(16)]
    public byte[] AddressUuid { get; set; } = null!;

    [Column("address_useruuid")]
    [MaxLength(16)]
    public byte[] AddressUseruuid { get; set; } = null!;

    [Column("address_name")]
    [StringLength(50)]
    public string AddressName { get; set; } = null!;

    [Column("address_phone")]
    [StringLength(255)]
    public string AddressPhone { get; set; } = null!;

    [Column("address_province")]
    [StringLength(50)]
    public string AddressProvince { get; set; } = null!;

    [Column("address_city")]
    [StringLength(50)]
    public string AddressCity { get; set; } = null!;

    [Column("address_district")]
    [StringLength(50)]
    public string AddressDistrict { get; set; } = null!;

    [Column("address_detail")]
    [StringLength(255)]
    public string AddressDetail { get; set; } = null!;

    [Required]
    [Column("address_isdefault")]
    public bool? AddressIsdefault { get; set; }

    [Column("address_time", TypeName = "datetime")]
    public DateTime AddressTime { get; set; }

    [Column("address_isdeleted")]
    public bool AddressIsdeleted { get; set; }

    [ForeignKey("AddressUseruuid")]
    [InverseProperty("Addresses")]
    public virtual User AddressUseruu { get; set; } = null!;
}
