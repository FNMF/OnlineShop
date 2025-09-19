using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("cart")]
[Index("Uuid", Name = "cart_user_user_uuid_fk")]
public partial class Cart
{
    [Key]
    [Column("cart_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("cart_useruuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("cart_time", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [Column("cart_merchantuuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("cart_isdeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("Uuid")]
    [InverseProperty("Carts")]
    public virtual User CartUseruu { get; set; } = null!;

    [InverseProperty("CartitemCartuu")]
    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();
}
