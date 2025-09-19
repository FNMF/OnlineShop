using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("wallet_transactions")]
[Index("MerchantUuid", Name = "wallettransaction_merchant_merchant_uuid_fk")]
public partial class WalletTransaction
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("wallet_transaction_type", TypeName = "enum('income','withdraw','refund','charge')")]
    public string WalletTransactionType { get; set; } = null!;

    [Column("amount")]
    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column("before")]
    [Precision(10, 2)]
    public decimal Before { get; set; }

    [Column("after")]
    [Precision(10, 2)]
    public decimal After { get; set; }

    [Column("object_uuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [Column("remark")]
    [StringLength(255)]
    public string? Remark { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("WalletTransactions")]
    public virtual Merchant MerchantUu { get; set; } = null!;
}
