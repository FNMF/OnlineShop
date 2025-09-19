using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("payment")]
[Index("OrderUuid", Name = "payment_order_order_uuid_fk")]
public partial class Payment
{
    [Key]
    [Column("payment_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("payment_orderuuid")]
    [MaxLength(16)]
    public Guid OrderUuid { get; set; }

    [Column("payment_amout")]
    [Precision(8, 2)]
    public decimal Amout { get; set; }

    [Column("payment_currency")]
    [StringLength(20)]
    public string Currency { get; set; } = null!;

    [Column("payment_status", TypeName = "enum('pending','accepted','rejected','exception')")]
    public string PaymentStatus { get; set; } = null!;

    [Column("payment_method")]
    [StringLength(50)]
    public string Method { get; set; } = null!;

    [Column("payment_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("payment_paidat", TypeName = "datetime")]
    public DateTime PaidAt { get; set; }

    [Column("payment_appid")]
    [StringLength(255)]
    public string AppId { get; set; } = null!;

    [Column("payment_mchid")]
    [StringLength(255)]
    public string MchId { get; set; } = null!;

    [Column("payment_outtradeno")]
    [StringLength(255)]
    public string OutTradeNo { get; set; } = null!;

    [Column("payment_transactionid")]
    [StringLength(255)]
    public string TransactionId { get; set; } = null!;

    [Column("payment_tradetype")]
    [StringLength(255)]
    public string TradeType { get; set; } = null!;

    [Column("payment_banktype")]
    [StringLength(255)]
    public string BankType { get; set; } = null!;

    [Column("payment_openid")]
    [StringLength(255)]
    public string OpenId { get; set; } = null!;

    [Column("payment_attach")]
    [StringLength(255)]
    public string? Attach { get; set; }

    [Column("payment_successtime", TypeName = "datetime")]
    public DateTime SuccessTime { get; set; }

    [Column("payment_rawcallbackdata")]
    [StringLength(255)]
    public string RawCallBackData { get; set; } = null!;

    [ForeignKey("OrderUuid")]
    [InverseProperty("Payments")]
    public virtual Order PaymentOrderuu { get; set; } = null!;
}
