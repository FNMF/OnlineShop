using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

/// <summary>
/// 区县表
/// </summary>
[Table("districts")]
[Index("CityId", Name = "fk_district_city")]
[Index("Name", "CityId", Name = "uk_district_name_city", IsUnique = true)]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class District
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 区县名称
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 所属城市ID
    /// </summary>
    [Column("city_id")]
    public int? CityId { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Districts")]
    public virtual City? City { get; set; }
}
