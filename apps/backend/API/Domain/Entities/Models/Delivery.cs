using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("delivery")]
[Index("Uuid", Name = "delivery_notification_notification_uuid_fk")]
public partial class Delivery
{
    [Key]
    [Column("delivery_uuid")]
    [MaxLength(16)]
    public Guid Uuid { get; set; }

    [Column("delivery_notificationuuid")]
    [MaxLength(16)]
    public Guid NotificationUuid { get; set; }

    [Column("delivery_receiveruuid")]
    [MaxLength(16)]
    public Guid ReceiverUuid { get; set; }

    [Column("delivery_isread")]
    public bool IsRead { get; set; }

    [Column("delivery_createdat", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("delivery_readat", TypeName = "datetime")]
    public DateTime ReadAt { get; set; }

    [ForeignKey("Uuid")]
    [InverseProperty("Deliveries")]
    public virtual Notification DeliveryNotificationuu { get; set; } = null!;
}
