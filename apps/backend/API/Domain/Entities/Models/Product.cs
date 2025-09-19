using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("product")]
[Index("MerchantUuid", Name = "product_merchant_merchant_uuid_fk")]
public partial class Product
{
    [Key]
    [Column("product_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("product_name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Column("product_price")]
    [Precision(8, 2)]
    public decimal Price { get; set; }

    [Column("product_stock")]
    public int Stock { get; set; }

    [Column("product_description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("product_isavailable")]
    public bool IsAvailable { get; set; }

    [Column("product_ingredient")]
    [StringLength(255)]
    public string? Ingredient { get; set; }

    [Column("product_weight")]
    [StringLength(255)]
    public string Weight { get; set; } = null!;

    [Column("product_islisted")]
    public bool IsListed { get; set; }

    [Column("product_merchantuuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("product_coverurl")]
    [StringLength(255)]
    public string? CoverUrl { get; set; }

    [Column("product_time", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("product_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("product_isaudited")]
    public bool IsAudited { get; set; }

    [Column("product_packingfee")]
    [Precision(8, 2)]
    public decimal PackingFee { get; set; }

    [Column("product_id")]
    public int? Id { get; set; }

    [InverseProperty("OrderitemProductuu")]
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    [ForeignKey("MerchantUuid")]
    [InverseProperty("Products")]
    public virtual Merchant ProductMerchantuu { get; set; } = null!;
}
