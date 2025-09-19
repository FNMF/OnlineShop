using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("merchants")]
[Index("AdminUuid", Name = "merchant_admin_admin_uuid_fk")]
public partial class Merchant
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("province")]
    [StringLength(50)]
    public string Province { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("district")]
    [StringLength(50)]
    public string District { get; set; } = null!;

    [Column("detail")]
    [StringLength(255)]
    public string Detail { get; set; } = null!;

    [Column("business_start", TypeName = "time")]
    public TimeOnly BusinessStart { get; set; }

    [Column("business_end", TypeName = "time")]
    public TimeOnly BusinessEnd { get; set; }

    [Column("admin_uuid")]
    [MaxLength(16)]
    public Guid AdminUuid { get; set; }

    [Required]
    [Column("is_closed")]
    public bool? IsClosed { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("is_audited")]
    public bool IsAudited { get; set; }

    [ForeignKey("AdminUuid")]
    [InverseProperty("Merchants")]
    public virtual Admin AdminUu { get; set; } = null!;

    [InverseProperty("MerchantUu")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("MerchantUu")]
    public virtual ICollection<WalletAccount> WalletAccounts { get; set; } = new List<WalletAccount>();

    [InverseProperty("MerchantUu")]
    public virtual ICollection<WalletRequest> WalletRequests { get; set; } = new List<WalletRequest>();

    [InverseProperty("MerchantUu")]
    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
