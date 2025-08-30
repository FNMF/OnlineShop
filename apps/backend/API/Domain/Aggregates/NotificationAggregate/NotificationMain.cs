using API.Common.Helpers;
using API.Domain.Enums;

namespace API.Domain.Aggregates.NotificationAggregate
{
    public class NotificationMain
    {
        public Guid NotificationUuid { get; private set; }
        public NotificationType Type { get;private set; }
        public string Title { get;private set; }
        public string Content { get;private set; }
        public DateTime StartTime { get;private set; }
        public DateTime EndTime { get; private set; }
        public NotificationReceiverType NotificationReceiverType { get;private set; }
        public Guid? ObjectUuid { get;private set; }
        public NotificationSenderType NotificationSenderType { get;private set; }
        public Guid? SenderUuid { get;private set; }
        public bool IsDeleted { get; private set; }
        public bool IsAudited { get; private set; }
        public DateTime CreatedAt { get; private set; }
        private readonly List<NotificationDelivery> _deliveries = new List<NotificationDelivery>();
        public IReadOnlyList<NotificationDelivery> Deliveries => _deliveries.AsReadOnly();

        public NotificationMain(NotificationType type, string title, string content, DateTime startTime, DateTime endTime, NotificationReceiverType notificationReceiverType, Guid? objectUuid, NotificationSenderType notificationSenderType, Guid? senderUuid, List<NotificationDelivery> deliveries)
        {
            NotificationUuid = UuidV7Helper.NewUuidV7();
            Type = type;
            Title = title;
            Content = content;
            StartTime = startTime;
            EndTime = endTime;
            NotificationReceiverType = notificationReceiverType;
            ObjectUuid = objectUuid;
            NotificationSenderType = notificationSenderType;
            SenderUuid = senderUuid;
            IsDeleted = false;
            IsAudited = false;
            CreatedAt = DateTime.UtcNow;
            _deliveries = deliveries ?? new List<NotificationDelivery>();
        }
        internal NotificationMain(Guid notificationUuid, NotificationType type, string title, string content, DateTime startTime, DateTime endTime, NotificationReceiverType notificationReceiverType, Guid? objectUuid, NotificationSenderType notificationSenderType, Guid? senderUuid, bool isDeleted, bool isAudited, DateTime createdAt, List<NotificationDelivery> deliveries)
        {
            NotificationUuid = notificationUuid;
            Type = type;
            Title = title;
            Content = content;
            StartTime = startTime;
            EndTime = endTime;
            NotificationReceiverType = notificationReceiverType;
            ObjectUuid = objectUuid;
            NotificationSenderType = notificationSenderType;
            SenderUuid = senderUuid;
            IsDeleted = isDeleted;
            IsAudited = isAudited;
            CreatedAt = createdAt;
            _deliveries = deliveries ?? new List<NotificationDelivery>();
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
        public void MarkAsAudited()
        {
            IsAudited = true;
        }

    }
}
