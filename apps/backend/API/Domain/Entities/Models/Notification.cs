using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("notification")]
public partial class Notification
{
    [Key]
    [Column("notification_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("notification_title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("notification_content")]
    [StringLength(255)]
    public string Content { get; set; } = null!;

    [Column("notification_type", TypeName = "enum('order','system','activity')")]
    public string NotificationType { get; set; } = null!;

    [Column("notification_starttime", TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column("notification_isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("notification_receivertype", TypeName = "enum('user','merchant','alluser','allmerchant')")]
    public string NotificationReceiverType { get; set; } = null!;

    [Column("notification_sendertype", TypeName = "enum('merchant','platform','system','other')")]
    public string NotificationSenderType { get; set; } = null!;

    [Column("notification_senderuuid")]
    [MaxLength(16)]
    public Guid? SenderUuid { get; set; }

    [Column("notification_endtime", TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [Column("notification_isaudited")]
    public bool IsAudited { get; set; }

    [Column("notification_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("notification_objectuuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [InverseProperty("DeliveryNotificationuu")]
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
