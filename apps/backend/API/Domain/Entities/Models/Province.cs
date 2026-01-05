using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

/// <summary>
/// 省份表
/// </summary>
[Table("provinces")]
[Index("Name", Name = "uk_province_name", IsUnique = true)]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class Province
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 省份名称
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Province")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
