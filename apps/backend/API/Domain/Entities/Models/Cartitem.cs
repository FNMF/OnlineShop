using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("cartitem")]
[Index("CartitemCartuuid", Name = "cartitem_cart_cart_uuid_fk")]
public partial class Cartitem
{
    [Key]
    [Column("cartitem_uuid")]
    [MaxLength(16)]
    public Guid CartitemUuid { get; set; } 

    [Column("cartitem_cartuuid")]
    [MaxLength(16)]
    public Guid CartitemCartuuid { get; set; } 

    [Column("cartitem_productuuid")]
    [MaxLength(16)]
    public Guid CartitemProductuuid { get; set; } 

    [Column("cartitem_quantity")]
    public int CartitemQuantity { get; set; }

    [Column("cartitem_productname")]
    [StringLength(255)]
    public string CartitemProductname { get; set; } 

    [Column("cartitem_productprice")]
    [Precision(8, 2)]
    public decimal CartitemProductprice { get; set; }

    [Column("cartitem_productcover")]
    [StringLength(255)]
    public string CartitemProductcover { get; set; } 

    [Column("cartitem_createdat", TypeName = "datetime")]
    public DateTime CartitemCreatedat { get; set; }

    [Column("cartitem_isdeleted")]
    public bool CartitemIsdeleted { get; set; }

    [ForeignKey("CartitemCartuuid")]
    [InverseProperty("Cartitems")]
    public virtual Cart CartitemCartuu { get; set; } 
}
