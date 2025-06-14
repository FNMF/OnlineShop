using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Models;

[Table("merchant")]
[Index("MerchantAdminuuid", Name = "merchant_admin_admin_uuid_fk")]
public partial class Merchant
{
    [Key]
    [Column("merchant_uuid")]
    [MaxLength(16)]
    public byte[] MerchantUuid { get; set; } = null!;

    [Column("merchant_name")]
    [StringLength(50)]
    public string MerchantName { get; set; } = null!;

    [Column("merchant_province")]
    [StringLength(50)]
    public string MerchantProvince { get; set; } = null!;

    [Column("merchant_city")]
    [StringLength(50)]
    public string MerchantCity { get; set; } = null!;

    [Column("merchant_district")]
    [StringLength(50)]
    public string MerchantDistrict { get; set; } = null!;

    [Column("merchant_detail")]
    [StringLength(255)]
    public string MerchantDetail { get; set; } = null!;

    [Column("merchant_businessstart", TypeName = "datetime")]
    public DateTime MerchantBusinessstart { get; set; }

    [Column("merchant_businessend", TypeName = "datetime")]
    public DateTime MerchantBusinessend { get; set; }

    [Column("merchant_adminuuid")]
    [MaxLength(16)]
    public byte[]? MerchantAdminuuid { get; set; }

    [Required]
    [Column("merchant_isclosed")]
    public bool? MerchantIsclosed { get; set; }

    [Column("merchant_isdeleted")]
    public bool MerchantIsdeleted { get; set; }

    [ForeignKey("MerchantAdminuuid")]
    [InverseProperty("Merchants")]
    public virtual Admin? MerchantAdminuu { get; set; }

    [InverseProperty("ProductMerchantuu")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
