using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Models;

[Table("localfile")]
public partial class Localfile
{
    [Key]
    [Column("localfile_uuid")]
    [MaxLength(16)]
    public byte[] LocalfileUuid { get; set; } = null!;

    [Column("localfile_name")]
    [StringLength(255)]
    public string LocalfileName { get; set; } = null!;

    [Column("localfile_path")]
    [StringLength(255)]
    public string LocalfilePath { get; set; } = null!;

    [Column("localfile_type", TypeName = "enum('image','video','sound','log','other')")]
    public string LocalfileType { get; set; } = null!;

    [Column("localfile_time", TypeName = "datetime")]
    public DateTime LocalfileTime { get; set; }

    [Column("localfile_isdeleted")]
    public bool LocalfileIsdeleted { get; set; }

    [InverseProperty("ImageFileuu")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
