using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("orderitem")]
[Index("OrderUuid", Name = "orderitem_order_order_uuid_fk")]
[Index("Uuid", Name = "orderitem_product_product_uuid_fk")]
public partial class Orderitem
{
    [Key]
    [Column("orderitem_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("orderitem_orderuuid")]
    [MaxLength(16)]
    public Guid OrderUuid { get; set; }

    [Column("orderitem_productuuid")]
    [MaxLength(16)]
    public Guid ProductUuid { get; set; }

    [Column("orderitem_quantity")]
    public int Quantity { get; set; }

    [Column("orderitem_price")]
    [Precision(8, 2)]
    public decimal Price { get; set; }

    [Column("orderitem_name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("orderitem_packingfee")]
    [Precision(8, 2)]
    public decimal PackingFee { get; set; }

    [ForeignKey("OrderUuid")]
    [InverseProperty("Orderitems")]
    public virtual Order OrderitemOrderuu { get; set; } = null!;

    [ForeignKey("Uuid")]
    [InverseProperty("Orderitems")]
    public virtual Product OrderitemProductuu { get; set; } = null!;
}
