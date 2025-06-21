using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("wallettransaction")]
[Index("WtMerchantuuid", Name = "wallettransaction_merchant_merchant_uuid_fk")]
public partial class Wallettransaction
{
    [Key]
    [Column("wt_uuid")]
    [MaxLength(16)]
    public byte[] WtUuid { get; set; } = null!;

    [Column("wt_merchantuuid")]
    [MaxLength(16)]
    public byte[] WtMerchantuuid { get; set; } = null!;

    [Column("wt_type", TypeName = "enum('income','withdraw','refund','charge')")]
    public string WtType { get; set; } = null!;

    [Column("wt_amount")]
    [Precision(10, 2)]
    public decimal WtAmount { get; set; }

    [Column("wt_before")]
    [Precision(10, 2)]
    public decimal WtBefore { get; set; }

    [Column("wt_after")]
    [Precision(10, 2)]
    public decimal WtAfter { get; set; }

    [Column("wt_objectuuid")]
    [MaxLength(16)]
    public byte[]? WtObjectuuid { get; set; }

    [Column("wt_remark")]
    [StringLength(255)]
    public string? WtRemark { get; set; }

    [Column("wt_createdat", TypeName = "datetime")]
    public DateTime WtCreatedat { get; set; }

    [ForeignKey("WtMerchantuuid")]
    [InverseProperty("Wallettransactions")]
    public virtual Merchant WtMerchantuu { get; set; } = null!;
}
