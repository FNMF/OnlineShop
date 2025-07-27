using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("payment")]
[Index("PaymentOrderuuid", Name = "payment_order_order_uuid_fk")]
public partial class Payment
{
    [Key]
    [Column("payment_uuid")]
    [MaxLength(16)]
    public byte[] PaymentUuid { get; set; } = null!;

    [Column("payment_orderuuid")]
    [MaxLength(16)]
    public byte[] PaymentOrderuuid { get; set; } = null!;

    [Column("payment_amout")]
    [Precision(8, 2)]
    public decimal PaymentAmout { get; set; }

    [Column("payment_currency")]
    [StringLength(20)]
    public string PaymentCurrency { get; set; } = null!;

    [Column("payment_status", TypeName = "enum('pending','accepted','rejected','exception')")]
    public string PaymentStatus { get; set; } = null!;

    [Column("payment_method")]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [Column("payment_createdat", TypeName = "datetime")]
    public DateTime PaymentCreatedat { get; set; }

    [Column("payment_paidat", TypeName = "datetime")]
    public DateTime PaymentPaidat { get; set; }

    [Column("payment_appid")]
    [StringLength(255)]
    public string PaymentAppid { get; set; } = null!;

    [Column("payment_mchid")]
    [StringLength(255)]
    public string PaymentMchid { get; set; } = null!;

    [Column("payment_outtradeno")]
    [StringLength(255)]
    public string PaymentOuttradeno { get; set; } = null!;

    [Column("payment_transactionid")]
    [StringLength(255)]
    public string PaymentTransactionid { get; set; } = null!;

    [Column("payment_tradetype")]
    [StringLength(255)]
    public string PaymentTradetype { get; set; } = null!;

    [Column("payment_banktype")]
    [StringLength(255)]
    public string PaymentBanktype { get; set; } = null!;

    [Column("payment_openid")]
    [StringLength(255)]
    public string PaymentOpenid { get; set; } = null!;

    [Column("payment_attach")]
    [StringLength(255)]
    public string? PaymentAttach { get; set; }

    [Column("payment_successtime", TypeName = "datetime")]
    public DateTime PaymentSuccesstime { get; set; }

    [Column("payment_rawcallbackdata")]
    [StringLength(255)]
    public string PaymentRawcallbackdata { get; set; } = null!;

    [ForeignKey("PaymentOrderuuid")]
    [InverseProperty("Payments")]
    public virtual Order PaymentOrderuu { get; set; } = null!;
}
