using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("wallet_requests")]
[Index("MerchantUuid", Name = "walletrequest_merchant_merchant_uuid_fk")]
public partial class WalletRequest
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("amount")]
    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column("wallet_request_status", TypeName = "enum('pending','approved','rejected','paid')")]
    public string WalletRequestStatus { get; set; } = null!;

    [Column("reason")]
    [StringLength(255)]
    public string? Reason { get; set; }

    [Column("audited_at", TypeName = "datetime")]
    public DateTime? AuditedAt { get; set; }

    [Column("transfer_time", TypeName = "datetime")]
    public DateTime? TransferTime { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("WalletRequests")]
    public virtual Merchant MerchantUu { get; set; } = null!;
}
