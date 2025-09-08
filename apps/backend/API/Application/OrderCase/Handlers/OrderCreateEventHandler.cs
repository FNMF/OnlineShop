using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.SignalR;
using API.Common.Interfaces;
using API.Domain.Aggregates.NotificationAggregate;
using API.Domain.Aggregates.NotificationAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregate.OrderEvents;
using System.Text.Json;

namespace API.Application.OrderCase.Handlers
{
    public class OrderCreateEventHandler:IEventHandler<OrderCreateEvent>
    {
        private readonly INotificationCreateService _notificationCreateService;
        private readonly INotificationHubService _notificationHubService;
        private readonly ILogService _logService;
        private readonly ILogger<OrderCreateEventHandler> _logger;

        public OrderCreateEventHandler(INotificationCreateService notificationCreateService, INotificationHubService notificationHubService, ILogService logService, ILogger<OrderCreateEventHandler> logger)
        {
            _notificationCreateService = notificationCreateService;
            _notificationHubService = notificationHubService;
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(OrderCreateEvent @event, CancellationToken cancellation = default)
        {
            try
            {
                //创建通知
                var notificationCreateDto = new NotificationCreateDto
                {
                    Type = Domain.Enums.NotificationType.order,
                    Title = "用户发起订单",
                    Content = $"用户订单 {@event.OrderMain.OrderUuid.ToString()} 已创建成功，订单金额为 {@event.OrderMain.OrderTotal} 元。",
                    StartTime = @event.OccurredOn,
                    EndTime = @event.OccurredOn.AddMinutes(30),
                    NotificationReceiverType = Domain.Enums.NotificationReceiverType.merchant,
                    ObjectUuid = @event.OrderMain.OrderUuid,
                    NotificationSenderType = Domain.Enums.NotificationSenderType.user,
                    SenderUuid = @event.OrderMain.OrderUseruuid
                };

                var notificationCreateResult = await _notificationCreateService.AddNotificationAsync(notificationCreateDto);
                if(!notificationCreateResult.IsSuccess)
                {
                    _logger.LogError($"创建订单通知失败: {notificationCreateResult.Message}");
                    return;
                }
                var notificationMain = notificationCreateResult.Data;
                var delivery = new NotificationDelivery
                    (
                        notificationMain.NotificationUuid,
                        @event.MerchantUuid
                    );
                var deliverys = new List<NotificationDelivery> { delivery };
                var deliveryAddResult = _notificationCreateService.AddDeliveriesAsync(notificationMain , deliverys);

                // SignalR 通知商户
                await _notificationHubService.NotifyOrderCreatedAsync(@event.MerchantUuid, notificationMain);
                // 记录日志
                await _logService.AddLog(Domain.Enums.LogType.order, "订单创建", @event.OrderMain.OrderUseruuid.ToString(), @event.OrderMain.OrderUuid,JsonSerializer.Serialize( @event.OrderMain));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"执行OrderCreate事件时出错 '{@event.OrderMain.OrderUseruuid.ToString()}' 用户Uuid '{@event.OrderMain.OrderUseruuid.ToString()}'。");
            }
        }
    }
}
