using API.Domain.Enums;

namespace API.Api.Models
{
    public class NotificationQueryOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public byte[]? UuidBytes { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationReceiverType? ReceiverType { get; set; }
    }
}
