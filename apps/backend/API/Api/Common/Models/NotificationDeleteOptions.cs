using API.Domain.Enums;

namespace API.Api.Common.Models
{
    public class NotificationDeleteOptions
    {
        public Guid? Uuid { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationReceiverType? ReceiverType { get; set; }
        public Guid? SenderUuid { get; set; }
        public bool? IsAudited { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
