using API.Domain.Enums;

namespace API.Api.Models
{
    public class NotificationDeleteOptions
    {
        public byte[]? UuidBytes { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationReceiverType? ReceiverType { get; set; }
        public NotificationSenderType? SenderType { get; set; }      
        public byte[]? SenderUuidBytes { get; set; }                
        public bool? IsAudited { get; set; }                          
        public bool? IsDeleted { get; set; }
    }
}
