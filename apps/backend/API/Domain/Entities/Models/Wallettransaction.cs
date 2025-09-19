using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("wallettransaction")]
[Index("MerchantUuid", Name = "wallettransaction_merchant_merchant_uuid_fk")]
public partial class WalletTransaction
{
    [Key]
    [Column("wt_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("wt_merchantuuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; }

    [Column("wt_type", TypeName = "enum('income','withdraw','refund','charge')")]
    public string WalletTransactionType { get; set; } = null!;

    [Column("wt_amount")]
    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column("wt_before")]
    [Precision(10, 2)]
    public decimal Before { get; set; }

    [Column("wt_after")]
    [Precision(10, 2)]
    public decimal After { get; set; }

    [Column("wt_objectuuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [Column("wt_remark")]
    [StringLength(255)]
    public string? Remark { get; set; }

    [Column("wt_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("MerchantUuid")]
    [InverseProperty("Wallettransactions")]
    public virtual Merchant WtMerchantuu { get; set; } = null!;
}
