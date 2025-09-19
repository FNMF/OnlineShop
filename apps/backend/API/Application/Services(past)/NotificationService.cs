using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;
        private readonly ILogService _logService;
        private readonly ICurrentService _currentService;

        public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger,ILogService logService, ICurrentService currentService)
        {
            _notificationRepository = notificationRepository;
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
                            Title = commandDto.Title,
                            Content = commandDto.Content,
                            StartTime =commandDto.StartTime,
                            EndTime =commandDto.EndTime,
                            CreatedAt =DateTime.Now,
                            NotificationType =NotificationType.system.ToString(),
                            NotificationSenderType =NotificationSenderType.system.ToString(),
                            NotificationReceiverType = commandDto.ReceiverType.ToString(),
                            Uuid = UuidV7Helper.NewUuidV7()
                        };
                        await _notificationRepository.AddNotificationAsync(notification);
                        await _logService.AddLog(LogType.admin, "系统添加通知", "",notification.Uuid);
                        return true;
                    }
                    else if (_currentService.CurrentType == CurrentType.Platform)
                    {
                        // 平台业务逻辑
                        var notification = new Notification
                        {
                            Title = commandDto.Title,
                            Content = commandDto.Content,
                            StartTime = commandDto.StartTime,
                            EndTime = commandDto.EndTime,
                            CreatedAt = DateTime.Now,
                            NotificationType = NotificationType.activity.ToString(),
                            NotificationSenderType = NotificationSenderType.platform.ToString(),
                            NotificationReceiverType = commandDto.ReceiverType.ToString(),
                            Uuid = UuidV7Helper.NewUuidV7()
                        };
                        await _notificationRepository.AddNotificationAsync(notification);
                        await _logService.AddLog(LogType.admin,"平台添加通知","",notification.Uuid);
                        return true;
                    }
                    else if (_currentService.CurrentType == CurrentType.Merchant)
                    {
                        // 商户业务逻辑
                        var notification = new Notification
                        {
                            Title = commandDto.Title,
                            Content = commandDto.Content,
                            StartTime = commandDto.StartTime,
                            EndTime = commandDto.EndTime,
                            CreatedAt = DateTime.Now,
                            NotificationType = NotificationType.activity.ToString(),
                            NotificationSenderType = NotificationSenderType.merchant.ToString(),
                            SenderUuid = _currentService.CurrentUuid,
                            NotificationReceiverType = commandDto.ReceiverType.ToString(),
                            Uuid = UuidV7Helper.NewUuidV7()
                        };
                        await _notificationRepository.AddNotificationAsync(notification);
                        await _logService.AddLog(LogType.merchant,"商户添加通知", "",notification.Uuid);
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
        public async Task<List<Notification>> GetNotifications(NotificationQueryOptions notificationQueryOptions)
        {
            try
            {
                if (notificationQueryOptions == null)
                {
                    throw new ArgumentNullException(nameof(notificationQueryOptions), "传入查询参数为空");
                }

                if (!_currentService.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("未登录用户无法查询通知");
                }

                var query = _notificationRepository.QueryNotifications();

                // 通用筛选条件
                query = query
                    .WhereIfNotNull(notificationQueryOptions.Type, x => x.NotificationType == notificationQueryOptions.Type.ToString())
                    .WhereIfNotNull(notificationQueryOptions.ReceiverType, x => x.NotificationReceiverType == notificationQueryOptions.ReceiverType.ToString())
                    .WhereIfNotNull(notificationQueryOptions.Title, x => x.Title.Contains(notificationQueryOptions.Title))
                    .WhereIfNotNull(notificationQueryOptions.StartTime, x => x.StartTime >= notificationQueryOptions.StartTime)
                    .WhereIfNotNull(notificationQueryOptions.EndTime, x => x.EndTime <= notificationQueryOptions.EndTime)
                    .WhereIfNotNull(notificationQueryOptions.IsAudited, x => x.IsAudited == notificationQueryOptions.IsAudited);

                // 鉴权过滤
                if (_currentService.CurrentType == CurrentType.Merchant)
                {
                    // 限定只查看自己的通知
                    if (_currentService.CurrentUuid != null)
                    {
                        query = query.Where(x => x.SenderUuid == _currentService.CurrentUuid);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("未知商户");
                    }
                }
                else if (_currentService.CurrentType == CurrentType.Platform || _currentService.CurrentType == CurrentType.System)
                {   
                    //只有平台或系统可以查看所有包括逻辑删除的通知
                    query = query.WhereIfNotNull(notificationQueryOptions.IsDeleted, x =>x.IsDeleted == notificationQueryOptions.IsDeleted);
                }
                else if (_currentService.CurrentType == CurrentType.User)
                {
                    throw new UnauthorizedAccessException("普通用户无法直接查询通知");
                }

                // 排序分页
                query = query.OrderByDescending(x => x.CreatedAt)
                             .PageBy(notificationQueryOptions.PageNumber, notificationQueryOptions.PageSize);

                var result = await query.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询通知失败");
                throw;
            }
        }
        public async Task<bool> RemoveNotificationManually(NotificationDeleteOptions notificationDeleteOptions)
        {
            try
            {
                if (!_currentService.IsAuthenticated || _currentService.CurrentType == CurrentType.User || _currentService.CurrentType == CurrentType.Other)
                {
                    throw new UnauthorizedAccessException("无权限删除通知");
                }

                if (notificationDeleteOptions == null || notificationDeleteOptions.Uuid == null)
                {
                    throw new ArgumentNullException(nameof(notificationDeleteOptions), "缺少通知标识");
                }

                var notification = await _notificationRepository.QueryNotifications()
                    .Where(n => n.Uuid == notificationDeleteOptions.Uuid)
                    .FirstOrDefaultAsync();

                if (notification == null)
                {
                    throw new Exception("通知不存在");
                }
                if(notification.IsDeleted == true) 
                {
                    throw new Exception("该通知已经被删除了");
                }

                // 鉴权：商户仅能删除自己的通知
                if (_currentService.CurrentType == CurrentType.Merchant && (_currentService.CurrentUuid == null || notification.SenderUuid != _currentService.CurrentUuid))
                {
                    throw new UnauthorizedAccessException("无法删除其他发送者的通知");
                }

                //逻辑删除，更改字段并更新数据
                notification.IsDeleted = true;
                await _notificationRepository.UpdateNotificationAsync(notification);
                
                if(_currentService.CurrentType == CurrentType.Merchant)
                {
                    await _logService.AddLog(LogType.merchant, "手动删除通知", "", notification.Uuid);
                }
                else
                {
                    await _logService.AddLog(LogType.admin, "手动删除通知", "", notification.Uuid);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除通知失败");
                return false;
            }
        }
        public async Task<bool> RemoveNotificationAutomatically()
        {
            try
            {
                var now = DateTime.Now;

                // 查询所有已过期但未被标记删除的通知
                var expiredNotifications = await _notificationRepository
                    .QueryNotifications()
                    .Where(n => n.EndTime < now.AddDays(-7) && n.IsDeleted == false)
                    .ToListAsync();

                if (!expiredNotifications.Any())
                    return true; // 无需删除

                foreach (var notification in expiredNotifications)
                {
                    notification.IsDeleted = true;
                }

                await _notificationRepository.SaveChangesAsync();

                await _logService.AddLog(LogType.admin, $"自动删除{expiredNotifications.Count}条过期通知","");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "自动删除过期通知失败");
                return false;
            }
        }
    }
}
