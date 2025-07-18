﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Entities.Models;

[Table("delivery")]
[Index("DeliveryNotificationuuid", Name = "delivery_notification_notification_uuid_fk")]
public partial class Delivery
{
    [Key]
    [Column("delivery_uuid")]
    [MaxLength(16)]
    public byte[] DeliveryUuid { get; set; } = null!;

    [Column("delivery_notificationuuid")]
    [MaxLength(16)]
    public byte[] DeliveryNotificationuuid { get; set; } = null!;

    [Column("delivery_receiveruuid")]
    [MaxLength(16)]
    public byte[] DeliveryReceiveruuid { get; set; } = null!;

    [Column("delivery_isread")]
    public bool DeliveryIsread { get; set; }

    [Column("delivery_createdat", TypeName = "datetime")]
    public DateTime DeliveryCreatedat { get; set; }

    [Column("delivery_readat", TypeName = "datetime")]
    public DateTime DeliveryReadat { get; set; }

    [ForeignKey("DeliveryNotificationuuid")]
    [InverseProperty("Deliveries")]
    public virtual Notification DeliveryNotificationuu { get; set; } = null!;
}
