using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("cartitems")]
[Index("CartUuid", Name = "cartitem_cart_cart_uuid_fk")]
public partial class Cartitem
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("cart_uuid")]
    [MaxLength(16)]
    public Guid CartUuid { get; set; }

    [Column("product_uuid")]
    [MaxLength(16)]
    public Guid ProductUuid { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("product_name")]
    [StringLength(255)]
    public string ProductName { get; set; } = null!;

    [Column("product_price")]
    [Precision(8, 2)]
    public decimal ProductPrice { get; set; }

    [Column("product_cover")]
    [StringLength(255)]
    public string ProductCover { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("packing_fee")]
    [Precision(8, 2)]
    public decimal PackingFee { get; set; }

    [ForeignKey("CartUuid")]
    [InverseProperty("Cartitems")]
    public virtual Cart CartUu { get; set; } = null!;
}
