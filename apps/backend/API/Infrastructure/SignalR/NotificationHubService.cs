using API.Api.SignalR;
using API.Application.SignalR;
using API.Domain.Aggregates.NotificationAggregate;
using Microsoft.AspNetCore.SignalR;

namespace API.Infrastructure.SignalR
{
    public class NotificationHubService:INotificationHubService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationHubService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyOrderCreatedAsync(Guid userUuid, NotificationMain notificationMain)
        {
            //将订单通知发送给指定用户
            await _hubContext.Clients.User(userUuid.ToString())
                .SendAsync("ReceiveNotification", notificationMain);
        }
    }
}
