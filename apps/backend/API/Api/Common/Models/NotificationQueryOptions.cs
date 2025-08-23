using API.Domain.Enums;

namespace API.Api.Common.Models
{
    public class NotificationQueryOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAudited { get; set; }
        public Guid? Uuid { get; set; }
        public Guid? SenderUuid { get; set; }
        public string? Title { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationReceiverType? ReceiverType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
