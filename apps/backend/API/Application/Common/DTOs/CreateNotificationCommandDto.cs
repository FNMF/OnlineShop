using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class CreateNotificationCommandDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public NotificationReceiverType ReceiverType { get; set; }

    }
}
