using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("orderitems")]
[Index("OrderUuid", Name = "orderitem_order_order_uuid_fk")]
[Index("ProductUuid", Name = "orderitem_product_product_uuid_fk")]
public partial class Orderitem
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("order_uuid")]
    [MaxLength(16)]
    public Guid OrderUuid { get; set; }

    [Column("product_uuid")]
    [MaxLength(16)]
    public Guid ProductUuid { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    [Precision(8, 2)]
    public decimal Price { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("packing_fee")]
    [Precision(8, 2)]
    public decimal PackingFee { get; set; }

    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [ForeignKey("OrderUuid")]
    [InverseProperty("Orderitems")]
    public virtual Order OrderUu { get; set; } = null!;

    [ForeignKey("ProductUuid")]
    [InverseProperty("Orderitems")]
    public virtual Product ProductUu { get; set; } = null!;
}
