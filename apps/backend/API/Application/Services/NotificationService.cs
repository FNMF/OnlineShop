using API.Application.DTOs;
using API.Application.Interfaces;
using API.Common.Helpers;
using API.Domain.Entities.Models;
using API.Domain.Enums;

namespace API.Application.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;
        private readonly ILogService _logService;
        private readonly ICurrentService _currentService;

        public NotificationService(INotificationService notificationService, ILogger logger,ILogService logService, ICurrentService currentService)
        {
            _notificationService = notificationService;
            _logger = logger;
            _logService = logService;
            _currentService = currentService;
        }
        public async Task<bool> AddNotification(CreateNotificationCommandDto commandDto)
        {
            try
            {
                if (!_currentService.IsAuthenticated || _currentService.CurrentType == CurrentType.User || _currentService.CurrentType == CurrentType.Other)
                {
                    throw new UnauthorizedAccessException("无权限发送通知");
                }
                if (commandDto.EndTime <= DateTime.Now)
                {
                    throw new Exception("通知结束时间不能早于当前时间");
                }
                if (string.IsNullOrWhiteSpace(commandDto.Title) || commandDto.Title.Length > 255)
                {
                    throw new Exception("标题不能为空，且不能超过255字符");
                }
                    
                try
                {
                    if(_currentService.CurrentType == CurrentType.System)
                    {
                        //系统业务逻辑
                        var notification = new Notification
                        {
                            NotificationTitle = commandDto.Title,
                            NotificationContent = commandDto.Content,
                            NotificationStarttime =commandDto.StartTime,
                            NotificationEndtime =commandDto.EndTime,
                            NotificationCreatedat =DateTime.Now,
                            NotificationType =NotificationType.system.ToString(),
                            NotificationSendertype =NotificationSenderType.system.ToString(),
                            NotificationReceivertype = commandDto.ReceiverType.ToString(),
                            NotificationUuid = UuidV7Helper.NewUuidV7ToBtyes()
                        };
                        await _notificationService.AddNotification(notification);
                        await _logService.AddLog(LogType.admin, "系统添加通知", "",notification.NotificationUuid);
                        return true;
                    }
                    else if (_currentService.CurrentType == CurrentType.Platform)
                    {
                        // 平台业务逻辑
                        var notification = new Notification
                        {
                            NotificationTitle = commandDto.Title,
                            NotificationContent = commandDto.Content,
                            NotificationStarttime = commandDto.StartTime,
                            NotificationEndtime = commandDto.EndTime,
                            NotificationCreatedat = DateTime.Now,
                            NotificationType = NotificationType.activity.ToString(),
                            NotificationSendertype = NotificationSenderType.platform.ToString(),
                            NotificationReceivertype = commandDto.ReceiverType.ToString(),
                            NotificationUuid = UuidV7Helper.NewUuidV7ToBtyes()
                        };
                        await _notificationService.AddNotification(notification);
                        await _logService.AddLog(LogType.admin,"平台添加通知","",notification.NotificationUuid);
                        return true;
                    }
                    else if (_currentService.CurrentType == CurrentType.Merchant)
                    {
                        // 商户业务逻辑
                        var notification = new Notification
                        {
                            NotificationTitle = commandDto.Title,
                            NotificationContent = commandDto.Content,
                            NotificationStarttime = commandDto.StartTime,
                            NotificationEndtime = commandDto.EndTime,
                            NotificationCreatedat = DateTime.Now,
                            NotificationType = NotificationType.activity.ToString(),
                            NotificationSendertype = NotificationSenderType.merchant.ToString(),
                            NotificationSenderuuid = _currentService.CurrentUuid,
                            NotificationReceivertype = commandDto.ReceiverType.ToString(),
                            NotificationUuid = UuidV7Helper.NewUuidV7ToBtyes()
                        };
                        await _notificationService.AddNotification(notification);
                        await _logService.AddLog(LogType.merchant,"商户添加通知", "",notification.NotificationUuid);
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning("未识别的发送者类型：{CurrentType}", _currentService.CurrentType);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "添加Notification失败");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "鉴权失败");
                throw;
            }
        }
    }
}
