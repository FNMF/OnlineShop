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
    public Guid ProductUuid { get; set; }

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
    public string? ProductIngredient { get; set; }

    [Column("product_weight")]
    [StringLength(255)]
    public string ProductWeight { get; set; } = null!;

    [Column("product_islisted")]
    public bool ProductIslisted { get; set; }

    [Column("product_merchantuuid")]
    [MaxLength(16)]
    public Guid ProductMerchantuuid { get; set; }

    [Column("product_coverurl")]
    [StringLength(255)]
    public string? ProductCoverurl { get; set; }

    [Column("product_time", TypeName = "datetime")]
    public DateTime ProductTime { get; set; }

    [Column("product_isdeleted")]
    public bool ProductIsdeleted { get; set; }

    [Column("product_isaudited")]
    public bool ProductIsaudited { get; set; }

    [InverseProperty("OrderitemProductuu")]
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    [ForeignKey("ProductMerchantuuid")]
    [InverseProperty("Products")]
    public virtual Merchant ProductMerchantuu { get; set; } = null!;
}
