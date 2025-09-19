using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("notifications")]
public partial class Notification
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("content")]
    [StringLength(255)]
    public string Content { get; set; } = null!;

    [Column("notification_type", TypeName = "enum('order','system','activity')")]
    public string NotificationType { get; set; } = null!;

    [Column("start_time", TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("notification_receiver_type", TypeName = "enum('user','merchant','alluser','allmerchant')")]
    public string NotificationReceiverType { get; set; } = null!;

    [Column("notification_sender_type", TypeName = "enum('user','merchant','platform','system','other')")]
    public string NotificationSenderType { get; set; } = null!;

    [Column("sender_uuid")]
    [MaxLength(16)]
    public Guid? SenderUuid { get; set; }

    [Column("end_time", TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [Column("is_audited")]
    public bool IsAudited { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("object_uuid")]
    [MaxLength(16)]
    public Guid? ObjectUuid { get; set; }

    [InverseProperty("NotificationUu")]
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
