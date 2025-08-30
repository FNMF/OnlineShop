using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class NotificationCreateDto
    {
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public NotificationReceiverType NotificationReceiverType { get; set; }
        public Guid? ObjectUuid { get; set; }
        public NotificationSenderType NotificationSenderType { get; set; }
        public Guid? SenderUuid { get; set; }
    }
}
