using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("image")]
[Index("ImageFileuuid", Name = "image_file_file_uuid_fk")]
[Index("ImageProductuuid", Name = "image_product_product_uuid_fk")]
public partial class Image
{
    [Key]
    [Column("image_uuid")]
    [MaxLength(16)]
    public byte[] ImageUuid { get; set; } = null!;

    [Column("image_type", TypeName = "enum('main','detail','thumbnail')")]
    public string ImageType { get; set; } = null!;

    [Column("image_fileuuid")]
    [MaxLength(16)]
    public byte[] ImageFileuuid { get; set; } = null!;

    [Column("image_productuuid")]
    [MaxLength(16)]
    public byte[] ImageProductuuid { get; set; } = null!;

    [Column("image_name")]
    [StringLength(255)]
    public string ImageName { get; set; } = null!;

    [Column("image_time", TypeName = "datetime")]
    public DateTime ImageTime { get; set; }

    [Column("image_isdeleted")]
    public bool ImageIsdeleted { get; set; }

    [ForeignKey("ImageFileuuid")]
    [InverseProperty("Images")]
    public virtual Localfile ImageFileuu { get; set; } = null!;

    [ForeignKey("ImageProductuuid")]
    [InverseProperty("Images")]
    public virtual Product ImageProductuu { get; set; } = null!;
}
