﻿using API.Domain.Enums;

namespace API.Api.Common.Models
{
    public class NotificationQueryOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAudited { get; set; }
        public byte[]? UuidBytes { get; set; }
        public byte[]? SenderUuidBytes { get; set; }
        public string? Title { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationReceiverType? ReceiverType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
