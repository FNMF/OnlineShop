using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("merchant")]
[Index("AdminUuid", Name = "merchant_admin_admin_uuid_fk")]
public partial class Merchant
{
    [Key]
    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("merchant_name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("merchant_province")]
    [StringLength(50)]
    public string Province { get; set; } = null!;

    [Column("merchant_city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("merchant_district")]
    [StringLength(50)]
    public string District { get; set; } = null!;

    [Column("merchant_detail")]
    [StringLength(255)]
    public string Detail { get; set; } = null!;

    [Column("merchant_businessstart", TypeName = "time")]
    public TimeOnly BusinessStart { get; set; }

    [Column("merchant_businessend", TypeName = "time")]
    public TimeOnly BusinessEnd { get; set; }

    [Column("merchant_adminuuid")]
    [MaxLength(16)]
    public Guid AdminUuid { get; set; }

    [Required]
    [Column("merchant_isclosed")]
    public bool? IsClosed { get; set; }

    [Column("merchant_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("merchant_isaudited")]
    public bool IsAudited { get; set; }

    [ForeignKey("AdminUuid")]
    [InverseProperty("Merchants")]
    public virtual Admin MerchantAdminuu { get; set; } = null!;

    [InverseProperty("ProductMerchantuu")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("WaMerchantuu")]
    public virtual ICollection<WalletAccount> Walletaccounts { get; set; } = new List<WalletAccount>();

    [InverseProperty("WrMerchantuu")]
    public virtual ICollection<WalletRequest> Walletrequests { get; set; } = new List<WalletRequest>();

    [InverseProperty("WtMerchantuu")]
    public virtual ICollection<WalletTransaction> Wallettransactions { get; set; } = new List<WalletTransaction>();
}
