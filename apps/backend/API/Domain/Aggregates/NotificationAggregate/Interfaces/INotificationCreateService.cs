using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Domain.Aggregates.NotificationAggregate.Interfaces
{
    public interface INotificationCreateService
    {
        Task<Result<NotificationMain>> AddNotificationAsync(NotificationCreateDto dto);
        Result<NotificationMain> AddDeliveriesAsync(NotificationMain notificationMain, List<NotificationDelivery> deliveries);
    }
}
