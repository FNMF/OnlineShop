using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("products")]
[Index("MerchantUuid", Name = "product_merchant_merchant_uuid_fk")]
public partial class Product
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Column("price")]
    [Precision(8, 2)]
    public decimal Price { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("is_available")]
    public bool IsAvailable { get; set; }

    [Column("ingredient")]
    [StringLength(255)]
    public string? Ingredient { get; set; }

    [Column("weight")]
    [StringLength(255)]
    public string Weight { get; set; } = null!;

    [Column("is_listed")]
    public bool IsListed { get; set; }

    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("cover_url")]
    [StringLength(255)]
    public string? CoverUrl { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("is_audited")]
    public bool IsAudited { get; set; }

    [Column("packing_fee")]
    [Precision(8, 2)]
    public decimal PackingFee { get; set; }

    [Column("id")]
    public int? Id { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("Products")]
    public virtual Merchant MerchantUu { get; set; } = null!;

    [InverseProperty("ProductUu")]
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
