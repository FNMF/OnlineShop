using API.Common.Helpers;

namespace API.Domain.Aggregates.NotificationAggregate
{
    public class NotificationDelivery
    {
        public Guid DeliveryUuid { get; private set; }
        public Guid NotificationUuid { get; private set; }
        public Guid ReceiverUuid { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public NotificationDelivery(Guid notificationUuid, Guid receiverUuid)
        {
            DeliveryUuid = UuidV7Helper.NewUuidV7();
            NotificationUuid = notificationUuid;
            ReceiverUuid = receiverUuid;
            IsRead = false;
            ReadAt = null;
            CreatedAt = DateTime.UtcNow;
        }
        internal NotificationDelivery(Guid deliveryUuid, Guid notificationUuid, Guid receiverUuid, bool isRead, DateTime? readAt, DateTime createdAt)
        {
            DeliveryUuid = deliveryUuid;
            NotificationUuid = notificationUuid;
            ReceiverUuid = receiverUuid;
            IsRead = isRead;
            ReadAt = readAt;
            CreatedAt = createdAt;
        }
        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadAt = DateTime.UtcNow;
            }
        }
    }
}
