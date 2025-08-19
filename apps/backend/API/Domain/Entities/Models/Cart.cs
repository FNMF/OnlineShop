using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("cart")]
[Index("CartUseruuid", Name = "cart_user_user_uuid_fk")]
public partial class Cart
{
    [Key]
    [Column("cart_uuid")]
    [MaxLength(16)]
    public byte[] CartUuid { get; set; } = null!;

    [Column("cart_useruuid")]
    [MaxLength(16)]
    public byte[] CartUseruuid { get; set; } = null!;

    [Column("cart_time", TypeName = "datetime")]
    public DateTime CartTime { get; set; }

    [Column("cart_merchantuuid")]
    [MaxLength(16)]
    public byte[] CartMerchantuuid { get; set; } = null!;

    [Column("cart_isdeleted")]
    public bool CartIsdeleted { get; set; }

    [ForeignKey("CartUseruuid")]
    [InverseProperty("Carts")]
    public virtual User CartUseruu { get; set; } = null!;

    [InverseProperty("CartitemCartuu")]
    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();
}
