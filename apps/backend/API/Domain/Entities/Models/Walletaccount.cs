using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("wallet_accounts")]
[Index("MerchantUuid", Name = "wallet_account_merchant_merchant_uuid_fk")]
public partial class WalletAccount
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("available")]
    [Precision(10, 2)]
    public decimal Available { get; set; }

    [Column("frozen")]
    [Precision(10, 2)]
    public decimal Frozen { get; set; }

    [Column("total_income")]
    [Precision(10, 2)]
    public decimal TotalIncome { get; set; }

    [Column("total_withdraw")]
    [Precision(10, 2)]
    public decimal TotalWithdraw { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("WalletAccounts")]
    public virtual Merchant MerchantUu { get; set; } = null!;
}
