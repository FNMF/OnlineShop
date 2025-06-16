using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("product")]
[Index("ProductMerchantuuid", Name = "product_merchant_merchant_uuid_fk")]
public partial class Product
{
    [Key]
    [Column("product_uuid")]
    [MaxLength(16)]
    public byte[] ProductUuid { get; set; } = null!;

    [Column("product_name")]
    [StringLength(30)]
    public string ProductName { get; set; } = null!;

    [Column("product_price")]
    [Precision(8, 2)]
    public decimal ProductPrice { get; set; }

    [Column("product_stock")]
    public int ProductStock { get; set; }

    [Column("product_description")]
    [StringLength(255)]
    public string? ProductDescription { get; set; }

    [Column("product_isavailable")]
    public bool ProductIsavailable { get; set; }

    [Column("product_ingredient")]
    [StringLength(255)]
    public string ProductIngredient { get; set; } = null!;

    [Column("product_weight")]
    [StringLength(255)]
    public string ProductWeight { get; set; } = null!;

    [Required]
    [Column("product_islisted")]
    public bool? ProductIslisted { get; set; }

    [Column("product_merchantuuid")]
    [MaxLength(16)]
    public byte[] ProductMerchantuuid { get; set; } = null!;

    [Column("product_coverurl")]
    [StringLength(255)]
    public string? ProductCoverurl { get; set; }

    [Column("product_time", TypeName = "datetime")]
    public DateTime ProductTime { get; set; }

    [Column("product_isdeleted")]
    public bool ProductIsdeleted { get; set; }

    [InverseProperty("ImageProductuu")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [ForeignKey("ProductMerchantuuid")]
    [InverseProperty("Products")]
    public virtual Merchant ProductMerchantuu { get; set; } = null!;
}
