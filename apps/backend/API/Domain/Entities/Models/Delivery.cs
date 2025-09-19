using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("deliveries")]
[Index("NotificationUuid", Name = "delivery_notification_notification_uuid_fk")]
public partial class Delivery
{
    [Key]
    [Column("uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("notification_uuid")]
    [MaxLength(16)]
    public Guid NotificationUuid { get; set; }

    [Column("receiver_uuid")]
    [MaxLength(16)]
    public Guid ReceiverUuid { get; set; }

    [Column("is_read")]
    public bool IsRead { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("read_at", TypeName = "datetime")]
    public DateTime? ReadAt { get; set; }

    [ForeignKey("NotificationUuid")]
    [InverseProperty("Deliveries")]
    public virtual Notification NotificationUu { get; set; } = null!;
}
