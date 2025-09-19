using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("cartitem")]
[Index("CartUuid", Name = "cartitem_cart_cart_uuid_fk")]
public partial class Cartitem
{
    [Key]
    [Column("cartitem_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("cartitem_cartuuid")]
    [MaxLength(16)]
    public Guid CartUuid { get; set; }

    [Column("cartitem_productuuid")]
    [MaxLength(16)]
    public Guid ProductUuid { get; set; }

    [Column("cartitem_quantity")]
    public int Quantity { get; set; }

    [Column("cartitem_productname")]
    [StringLength(255)]
    public string ProductName { get; set; } = null!;

    [Column("cartitem_productprice")]
    [Precision(8, 2)]
    public decimal ProductPrice { get; set; }

    [Column("cartitem_productcover")]
    [StringLength(255)]
    public string ProductCover { get; set; } = null!;

    [Column("cartitem_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("cartitem_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("cartitem_packingfee")]
    [Precision(8, 2)]
    public decimal PackingFee { get; set; }

    [ForeignKey("CartUuid")]
    [InverseProperty("Cartitems")]
    public virtual Cart CartitemCartuu { get; set; } = null!;
}
