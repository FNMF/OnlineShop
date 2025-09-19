using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("walletrequest")]
[Index("MerchantUuid", Name = "walletrequest_merchant_merchant_uuid_fk")]
public partial class WalletRequest
{
    [Key]
    [Column("wr_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("wr_merchantuuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("wr_amount")]
    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column("wr_status", TypeName = "enum('pending','approved','rejected','paid')")]
    public string WalletRequestStatus { get; set; } = null!;

    [Column("wr_reason")]
    [StringLength(255)]
    public string? Reason { get; set; }

    [Column("wr_audittime", TypeName = "datetime")]
    public DateTime? AuditedAt { get; set; }

    [Column("wr_transfertime", TypeName = "datetime")]
    public DateTime? TransferTime { get; set; }

    [Column("wr_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("Walletrequests")]
    public virtual Merchant WrMerchantuu { get; set; } = null!;
}
