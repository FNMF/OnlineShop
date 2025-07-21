using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("orderitem")]
[Index("OrderitemOrderuuid", Name = "orderitem_order_order_uuid_fk")]
[Index("OrderitemProductuuid", Name = "orderitem_product_product_uuid_fk")]
public partial class Orderitem
{
    [Key]
    [Column("orderitem_uuid")]
    [MaxLength(16)]
    public byte[] OrderitemUuid { get; set; } = null!;

    [Column("orderitem_orderuuid")]
    [MaxLength(16)]
    public byte[] OrderitemOrderuuid { get; set; } = null!;

    [Column("orderitem_productuuid")]
    [MaxLength(16)]
    public byte[] OrderitemProductuuid { get; set; } = null!;

    [Column("orderitem_quantity")]
    public int OrderitemQuantity { get; set; }

    [Column("orderitem_price")]
    [Precision(8, 2)]
    public decimal OrderitemPrice { get; set; }

    [Column("orderitem_name")]
    [StringLength(50)]
    public string OrderitemName { get; set; } = null!;

    [ForeignKey("OrderitemOrderuuid")]
    [InverseProperty("Orderitems")]
    public virtual Order OrderitemOrderuu { get; set; } = null!;

    [ForeignKey("OrderitemProductuuid")]
    [InverseProperty("Orderitems")]
    public virtual Product OrderitemProductuu { get; set; } = null!;
}
