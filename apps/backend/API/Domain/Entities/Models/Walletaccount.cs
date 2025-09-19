using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("walletaccount")]
[Index("MerchantUuid", Name = "wallet_account_merchant_merchant_uuid_fk")]
public partial class WalletAccount
{
    [Key]
    [Column("wa_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("wa_merchantuuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("wa_available")]
    [Precision(10, 2)]
    public decimal Available { get; set; }

    [Column("wa_frozen")]
    [Precision(10, 2)]
    public decimal Frozen { get; set; }

    [Column("wa_totalincome")]
    [Precision(10, 2)]
    public decimal TotalIncome { get; set; }

    [Column("wa_totalwithdraw")]
    [Precision(10, 2)]
    public decimal TotalWithdraw { get; set; }

    [Column("wa_updatedat", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("Walletaccounts")]
    public virtual Merchant WaMerchantuu { get; set; } = null!;
}
