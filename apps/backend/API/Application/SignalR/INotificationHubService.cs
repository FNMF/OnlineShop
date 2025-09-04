using API.Domain.Aggregates.NotificationAggregate;

namespace API.Application.SignalR
{
    public interface INotificationHubService
    {
        Task NotifyOrderCreatedAsync(Guid userUuid, NotificationMain notificationMain);
    }
}
