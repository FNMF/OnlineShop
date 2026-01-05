using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

/// <summary>
/// 城市表
/// </summary>
[Table("cities")]
[Index("ProvinceId", Name = "fk_city_province")]
[Index("Name", "ProvinceId", Name = "uk_city_name_province", IsUnique = true)]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 城市名称
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 所属省份ID
    /// </summary>
    [Column("province_id")]
    public int? ProvinceId { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [ForeignKey("ProvinceId")]
    [InverseProperty("Cities")]
    public virtual Province? Province { get; set; }
}
