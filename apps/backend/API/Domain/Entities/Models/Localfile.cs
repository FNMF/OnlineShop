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
    public Guid LocalfileUuid { get; set; } 

    [Column("localfile_name")]
    [StringLength(255)]
    public string LocalfileName { get; set; } 

    [Column("localfile_path")]
    [StringLength(255)]
    public string LocalfilePath { get; set; } 

    [Column("localfile_type", TypeName = "enum('image','video','audio','log','other')")]
    public string LocalfileType { get; set; } 

    [Column("localfile_createdat", TypeName = "datetime")]
    public DateTime LocalfileCreatedat { get; set; }

    [Column("localfile_isdeleted")]
    public bool LocalfileIsdeleted { get; set; }

    [Column("localfile_objectuuid")]
    [MaxLength(16)]
    public Guid? LocalfileObjectuuid { get; set; }

    [Column("localfile_objecttype", TypeName = "enum('merchant','product_cover','product_detail','user','platform','system')")]
    public string LocalfileObjecttype { get; set; } 

    [Column("localfile_mimetype")]
    [StringLength(50)]
    public string LocalfileMimetype { get; set; } 

    [Column("localfile_size")]
    public long LocalfileSize { get; set; }

    [Column("localfile_isaudited")]
    public bool LocalfileIsaudited { get; set; }

    [Column("localfile_uploaderuuid")]
    [MaxLength(16)]
    public Guid LocalfileUploaderuuid { get; set; } 

    [Column("localfile_uploadip")]
    [StringLength(50)]
    public string LocalfileUploadip { get; set; } 

    [Column("localfile_sort")]
    public int LocalfileSort { get; set; }
}
