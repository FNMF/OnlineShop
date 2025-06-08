using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.Models;

[Table("notification")]
[Index("NotificationUseruuid", Name = "notification_user_user_uuid_fk")]
public partial class Notification
{
    [Key]
    [Column("notification_uuid")]
    [MaxLength(16)]
    public byte[] NotificationUuid { get; set; } = null!;

    [Column("notification_useruuid")]
    [MaxLength(16)]
    public byte[] NotificationUseruuid { get; set; } = null!;

    [Column("notification_title")]
    [StringLength(255)]
    public string NotificationTitle { get; set; } = null!;

    [Column("notification_content")]
    [StringLength(255)]
    public string NotificationContent { get; set; } = null!;

    [Column("notification_type", TypeName = "enum('order','system','activity')")]
    public string NotificationType { get; set; } = null!;

    [Column("notification_isreaded")]
    public bool NotificationIsreaded { get; set; }

    [Column("notification_time", TypeName = "datetime")]
    public DateTime NotificationTime { get; set; }

    [Column("notification_isdeleted")]
    public bool NotificationIsdeleted { get; set; }

    [ForeignKey("NotificationUseruuid")]
    [InverseProperty("Notifications")]
    public virtual User NotificationUseruu { get; set; } = null!;
}
