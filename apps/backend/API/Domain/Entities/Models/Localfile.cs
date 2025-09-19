using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("localfiles")]
public partial class Localfile
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("path")]
    [StringLength(255)]
    public string Path { get; set; } = null!;

    [Column("localfile_type", TypeName = "enum('image','video','audio','log','other')")]
    public string LocalfileType { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("object_uuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [Column("localfile_object_type", TypeName = "enum('merchant','product_cover','product_detail','user','platform','system')")]
    public string LocalfileObjectType { get; set; } = null!;

    [Column("mime_type")]
    [StringLength(50)]
    public string MimeType { get; set; } = null!;

    [Column("size")]
    public long Size { get; set; }

    [Column("is_audited")]
    public bool IsAudited { get; set; }

    [Column("uploader_uuid")]
    [MaxLength(16)]
    public Guid UploaderUuid { get; set; }

    [Column("upload_ip")]
    [StringLength(50)]
    public string UploadIp { get; set; } = null!;

    [Column("sort_number")]
    public int SortNumber { get; set; }
}
