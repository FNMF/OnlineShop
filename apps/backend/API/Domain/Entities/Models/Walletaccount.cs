using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("walletaccount")]
[Index("WaMerchantuuid", Name = "wallet_account_merchant_merchant_uuid_fk")]
public partial class Walletaccount
{
    [Key]
    [Column("wa_uuid")]
    [MaxLength(16)]
    public Guid WaUuid { get; set; } 

    [Column("wa_merchantuuid")]
    [MaxLength(16)]
    public Guid WaMerchantuuid { get; set; } 

    [Column("wa_available")]
    [Precision(10, 2)]
    public decimal WaAvailable { get; set; }

    [Column("wa_frozen")]
    [Precision(10, 2)]
    public decimal WaFrozen { get; set; }

    [Column("wa_totalincome")]
    [Precision(10, 2)]
    public decimal WaTotalincome { get; set; }

    [Column("wa_totalwithdraw")]
    [Precision(10, 2)]
    public decimal WaTotalwithdraw { get; set; }

    [Column("wa_updatedat", TypeName = "datetime")]
    public DateTime WaUpdatedat { get; set; }

    [ForeignKey("WaMerchantuuid")]
    [InverseProperty("Walletaccounts")]
    public virtual Merchant WaMerchantuu { get; set; } 
}
