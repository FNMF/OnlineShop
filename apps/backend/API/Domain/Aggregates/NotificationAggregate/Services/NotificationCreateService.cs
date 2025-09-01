using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Aggregates.NotificationAggregate.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Aggregates.NotificationAggregate.Services
{
    public class NotificationCreateService: INotificationCreateService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationCreateService> _logger;

        public NotificationCreateService(INotificationRepository notificationRepository, ILogger<NotificationCreateService> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task<Result<NotificationMain>> AddNotificationAsync(NotificationCreateDto dto)
        {
            try
            {
                var createResult = NotificationFactory.CreateAggregate(dto);
                if (!createResult.IsSuccess)
                {
                    return Result<NotificationMain>.Fail(createResult.Code,createResult.Message);
                }
                var toEntityResult = NotificationFactory.ToEntity(createResult.Data);
                if (!await _notificationRepository.AddNotificationAsync(toEntityResult.Data)) 
                {
                    return Result<NotificationMain>.Fail(ResultCode.BusinessError, "创建通知失败");
                }
                return Result<NotificationMain>.Success(createResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<NotificationMain>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
        public Result<NotificationMain> AddDeliveriesAsync(NotificationMain notificationMain, List<NotificationDelivery> deliveries)
        {
            try
            {
                if (deliveries.Count == 0) 
                {
                    return Result<NotificationMain>.Fail(ResultCode.InvalidInput, "通知接收者不能为空");
                }
                foreach (var delivery in deliveries) 
                {
                    notificationMain.AddDelivery(delivery);
                }
                return Result<NotificationMain>.Success(notificationMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<NotificationMain>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
