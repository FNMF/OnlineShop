using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;

namespace API.Domain.Aggregates.NotificationAggregate
{
    public class NotificationFactory
    {
        public static Result<NotificationMain> CreateAggregate(NotificationCreateDto dto)
        {
            if (dto == null)
            {
                return Result<NotificationMain>.Fail(ResultCode.ValidationError, "输入数据不能为空");
            }
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length > 100)
            {
                return Result<NotificationMain>.Fail(ResultCode.ValidationError, "标题不能为空且长度不能超过100");
            }
            if (string.IsNullOrWhiteSpace(dto.Content) || dto.Content.Length > 255)
            {
                return Result<NotificationMain>.Fail(ResultCode.ValidationError, "内容不能为空且长度不能超过255");
            }
            if (dto.StartTime >= dto.EndTime)
            {
                return Result<NotificationMain>.Fail(ResultCode.ValidationError, "开始时间必须早于结束时间");
            }
            if (dto.NotificationSenderType == NotificationSenderType.user && dto.SenderUuid == null)
            {
                return Result<NotificationMain>.Fail(ResultCode.ValidationError, "发送者类型为用户时，发送者UUID不能为空");
            }
            if (dto.NotificationSenderType == NotificationSenderType.merchant && dto.SenderUuid == null)
            {
                return Result<NotificationMain>.Fail(ResultCode.ValidationError, "发送者类型为商户时，发送者UUID不能为空");
            }
            var notificationMain = new NotificationMain(
                dto.Type,
                dto.Title,
                dto.Content,
                dto.StartTime,
                dto.EndTime,
                dto.NotificationReceiverType,
                dto.ObjectUuid,
                dto.NotificationSenderType,
                dto.SenderUuid,
                new List<NotificationDelivery>()
            );

            return Result<NotificationMain>.Success(notificationMain);
        }

        public static Result<Notification> ToEntity(NotificationMain notificationMain)
        {
            if (notificationMain == null)
            {
                return Result<Notification>.Fail(ResultCode.InvalidInput, "输入为空");
            }
            var notification = new Notification
            {
                Uuid = notificationMain.NotificationUuid,
                NotificationType = notificationMain.Type.ToString(),
                Title = notificationMain.Title,
                Content = notificationMain.Content,
                StartTime = notificationMain.StartTime,
                EndTime = notificationMain.EndTime,
                NotificationReceiverType = notificationMain.NotificationReceiverType.ToString(),
                ObjectUuid = notificationMain.ObjectUuid,
                NotificationSenderType = notificationMain.NotificationSenderType.ToString(),
                SenderUuid = notificationMain.SenderUuid,
                IsDeleted = notificationMain.IsDeleted,
                IsAudited = notificationMain.IsAudited,
                CreatedAt = notificationMain.CreatedAt,
                Deliveries = notificationMain.Deliveries.Select(d => new Delivery
                {
                    Uuid = d.DeliveryUuid,
                    NotificationUuid = d.NotificationUuid,
                    ReceiverUuid = d.ReceiverUuid,
                    IsRead = d.IsRead,
                    ReadAt = d.CreatedAt,
                    CreatedAt = d.CreatedAt
                }).ToList()
            };
            return Result<Notification>.Success(notification);
        }

        public static Result<NotificationMain> ToAggregate(Notification notification)
        {
            if(notification == null)
            {
                return Result<NotificationMain>.Fail(ResultCode.InvalidInput, "输入为空");
            }
            var notificationMain = new NotificationMain(
                notification.Uuid,
                Enum.Parse<NotificationType>(notification.NotificationType),
                notification.Title,
                notification.Content,
                notification.StartTime,
                notification.EndTime,
                Enum.Parse<NotificationReceiverType>(notification.NotificationReceiverType),
                notification.ObjectUuid,
                Enum.Parse<NotificationSenderType>(notification.NotificationSenderType),
                notification.SenderUuid,
                notification.IsDeleted,
                notification.IsAudited,
                notification.CreatedAt,
                notification.Deliveries?.Select(d => new NotificationDelivery(
                    d.Uuid,
                    d.NotificationUuid,
                    d.ReceiverUuid,
                    d.IsRead,
                    d.ReadAt,
                    d.CreatedAt
                )).ToList() ?? new List<NotificationDelivery>()
            );
            return Result<NotificationMain>.Success(notificationMain);
        }
    }
}
