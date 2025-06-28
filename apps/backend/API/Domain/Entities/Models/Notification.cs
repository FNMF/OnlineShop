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
    public byte[] NotificationUuid { get; set; } = null!;

    [Column("notification_title")]
    [StringLength(255)]
    public string NotificationTitle { get; set; } = null!;

    [Column("notification_content")]
    [StringLength(255)]
    public string NotificationContent { get; set; } = null!;

    [Column("notification_type", TypeName = "enum('order','system','activity')")]
    public string NotificationType { get; set; } = null!;

    [Column("notification_starttime", TypeName = "datetime")]
    public DateTime NotificationStarttime { get; set; }

    [Column("notification_isdeleted")]
    public bool NotificationIsdeleted { get; set; }

    [Column("notification_receivertype", TypeName = "enum('user','merchant','alluser','allmerchant')")]
    public string NotificationReceivertype { get; set; } = null!;

    [Column("notification_sendertype", TypeName = "enum('merchant','platform','system','other')")]
    public string NotificationSendertype { get; set; } = null!;

    [Column("notification_senderuuid")]
    [MaxLength(16)]
    public byte[]? NotificationSenderuuid { get; set; }

    [Column("notification_endtime", TypeName = "datetime")]
    public DateTime NotificationEndtime { get; set; }

    [Column("notification_isaudited")]
    public bool NotificationIsaudited { get; set; }

    [Column("notification_createdat", TypeName = "datetime")]
    public DateTime NotificationCreatedat { get; set; }

    [InverseProperty("DeliveryNotificationuu")]
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
