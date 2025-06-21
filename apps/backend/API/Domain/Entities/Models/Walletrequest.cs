using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("walletrequest")]
[Index("WrMerchantuuid", Name = "walletrequest_merchant_merchant_uuid_fk")]
public partial class Walletrequest
{
    [Key]
    [Column("wr_uuid")]
    [MaxLength(16)]
    public byte[] WrUuid { get; set; } = null!;

    [Column("wr_merchantuuid")]
    [MaxLength(16)]
    public byte[] WrMerchantuuid { get; set; } = null!;

    [Column("wr_amount")]
    [Precision(10, 2)]
    public decimal WrAmount { get; set; }

    [Column("wr_status", TypeName = "enum('pending','approved','rejected','paid')")]
    public string WrStatus { get; set; } = null!;

    [Column("wr_reason")]
    [StringLength(255)]
    public string? WrReason { get; set; }

    [Column("wr_audittime", TypeName = "datetime")]
    public DateTime? WrAudittime { get; set; }

    [Column("wr_transfertime", TypeName = "datetime")]
    public DateTime? WrTransfertime { get; set; }

    [Column("wr_createdat", TypeName = "datetime")]
    public DateTime WrCreatedat { get; set; }

    [ForeignKey("WrMerchantuuid")]
    [InverseProperty("Walletrequests")]
    public virtual Merchant WrMerchantuu { get; set; } = null!;
}
