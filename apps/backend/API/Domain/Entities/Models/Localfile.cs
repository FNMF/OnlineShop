using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("localfile")]
public partial class Localfile
{
    [Key]
    [Column("localfile_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("localfile_name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("localfile_path")]
    [StringLength(255)]
    public string Path { get; set; } = null!;

    [Column("localfile_type", TypeName = "enum('image','video','audio','log','other')")]
    public string LocalfileType { get; set; } = null!;

    [Column("localfile_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("localfile_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("localfile_objectuuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [Column("localfile_objecttype", TypeName = "enum('merchant','product_cover','product_detail','user','platform','system')")]
    public string LocalfileObjectType { get; set; } = null!;

    [Column("localfile_mimetype")]
    [StringLength(50)]
    public string MimeType { get; set; } = null!;

    [Column("localfile_size")]
    public long Size { get; set; }

    [Column("localfile_isaudited")]
    public bool IsAudited { get; set; }

    [Column("localfile_uploaderuuid")]
    [MaxLength(16)]
    public Guid UploaderUuid { get; set; }

    [Column("localfile_uploadip")]
    [StringLength(50)]
    public string UploadIp { get; set; } = null!;

    [Column("localfile_sort")]
    public int SortNumber { get; set; }
}
