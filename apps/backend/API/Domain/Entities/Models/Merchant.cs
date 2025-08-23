using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("merchant")]
[Index("MerchantAdminuuid", Name = "merchant_admin_admin_uuid_fk")]
public partial class Merchant
{
    [Key]
    [Column("merchant_uuid")]
    [MaxLength(16)]
    public Guid MerchantUuid { get; set; } 

    [Column("merchant_name")]
    [StringLength(50)]
    public string MerchantName { get; set; } 

    [Column("merchant_province")]
    [StringLength(50)]
    public string MerchantProvince { get; set; } 

    [Column("merchant_city")]
    [StringLength(50)]
    public string MerchantCity { get; set; } 

    [Column("merchant_district")]
    [StringLength(50)]
    public string MerchantDistrict { get; set; } 

    [Column("merchant_detail")]
    [StringLength(255)]
    public string MerchantDetail { get; set; } 

    [Column("merchant_businessstart", TypeName = "time")]
    public TimeOnly MerchantBusinessstart { get; set; }

    [Column("merchant_businessend", TypeName = "time")]
    public TimeOnly MerchantBusinessend { get; set; }

    [Column("merchant_adminuuid")]
    [MaxLength(16)]
    public Guid MerchantAdminuuid { get; set; } 

    [Required]
    [Column("merchant_isclosed")]
    public bool? MerchantIsclosed { get; set; }

    [Column("merchant_isdeleted")]
    public bool MerchantIsdeleted { get; set; }

    [Column("merchant_isaudited")]
    public bool MerchantIsaudited { get; set; }

    [ForeignKey("MerchantAdminuuid")]
    [InverseProperty("Merchants")]
    public virtual Admin MerchantAdminuu { get; set; } 

    [InverseProperty("ProductMerchantuu")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("WaMerchantuu")]
    public virtual ICollection<Walletaccount> Walletaccounts { get; set; } = new List<Walletaccount>();

    [InverseProperty("WrMerchantuu")]
    public virtual ICollection<Walletrequest> Walletrequests { get; set; } = new List<Walletrequest>();

    [InverseProperty("WtMerchantuu")]
    public virtual ICollection<Wallettransaction> Wallettransactions { get; set; } = new List<Wallettransaction>();
}
