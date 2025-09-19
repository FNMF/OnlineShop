using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("carts")]
[Index("UserUuid", Name = "cart_user_user_uuid_fk")]
public partial class Cart
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("user_uuid")]
    [MaxLength(16)]
    public Guid UserUuid { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("CartUu")]
    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    [ForeignKey("UserUuid")]
    [InverseProperty("Carts")]
    public virtual User UserUu { get; set; } = null!;
}
