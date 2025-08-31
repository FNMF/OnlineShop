using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Aggregates.NotificationAggregate.Interfaces;
using API.Domain.Interfaces;
using API.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Aggregates.NotificationAggregate.Services
{
    public class NotificationReadService: INotificationReadService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationReadService> _logger;

        public NotificationReadService(INotificationRepository notificationRepository, ILogger<NotificationReadService> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }
        public async Task<Result<List<NotificationMain>>> GetUserAllNotificationsAsync(NotificationQueryOptions opt)
        {
            try
            {
                if (opt == null || opt.Uuid == Guid.Empty)
                {
                    return Result<List<NotificationMain>>.Fail(ResultCode.ValidationError, "输入参数错误");
                }

                var notifications = await _notificationRepository.QueryNotificationDeliveriesByUuidAsync(opt.Uuid.Value)
                    .Where(n => n.NotificationIsdeleted == false)
                    .Where(n => n.NotificationIsaudited == true)
                    .OrderByDescending( n => n.NotificationCreatedat)
                    .PageBy(opt.PageNumber, opt.PageSize)
                    .ToListAsync();
                
                var result = new List<NotificationMain>();
                foreach (var notification in notifications)
                {
                    var toMainResult = NotificationFactory.ToAggregate(notification);
                    if (toMainResult.IsSuccess)
                    {
                        result.Add(toMainResult.Data);
                    }
                }
                return Result<List<NotificationMain>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<NotificationMain>>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
