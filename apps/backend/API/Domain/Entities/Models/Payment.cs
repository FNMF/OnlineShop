using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("payments")]
[Index("OrderUuid", Name = "payment_order_order_uuid_fk")]
public partial class Payment
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("order_uuid")]
    [MaxLength(16)]
    public Guid OrderUuid { get; set; }

    [Column("amout")]
    [Precision(8, 2)]
    public decimal Amout { get; set; }

    [Column("currency")]
    [StringLength(20)]
    public string? Currency { get; set; }

    [Column("payment_status", TypeName = "enum('pending','accepted','rejected','exception')")]
    public string PaymentStatus { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("app_id")]
    [StringLength(255)]
    public string AppId { get; set; } = null!;

    [Column("mch_id")]
    [StringLength(255)]
    public string MchId { get; set; } = null!;

    [Column("out_trade_no")]
    [StringLength(255)]
    public string OutTradeNo { get; set; } = null!;

    [Column("transaction_id")]
    [StringLength(255)]
    public string? TransactionId { get; set; }

    [Column("trade_type")]
    [StringLength(255)]
    public string? TradeType { get; set; }

    [Column("bank_type")]
    [StringLength(255)]
    public string? BankType { get; set; }

    [Column("open_id")]
    [StringLength(255)]
    public string OpenId { get; set; } = null!;

    [Column("attach")]
    [StringLength(255)]
    public string? Attach { get; set; }

    [Column("success_time", TypeName = "datetime")]
    public DateTime? SuccessTime { get; set; }

    [Column("raw_call_back_data")]
    [StringLength(255)]
    public string? RawCallBackData { get; set; }

    [ForeignKey("OrderUuid")]
    [InverseProperty("Payments")]
    public virtual Order OrderUu { get; set; } = null!;
}
