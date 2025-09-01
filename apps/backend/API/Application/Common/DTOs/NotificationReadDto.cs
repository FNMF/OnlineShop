using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class NotificationReadDto
    {
        //NotificationDetails
        public Guid NotificationUuid { get; set; }
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public NotificationReceiverType NotificationReceiverType { get; set; }
        public Guid? ObjectUuid { get; set; }
        public NotificationSenderType NotificationSenderType { get; set; }
        public Guid? SenderUuid { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAudited { get; set; }
        public DateTime CreatedAt { get; set; }
        //DeliveryDetails
        public Guid DeliveryUuid { get; set; }
        public Guid ReceiverUuid { get; set; }
        public bool IsRead { get; set; }

    }
}
