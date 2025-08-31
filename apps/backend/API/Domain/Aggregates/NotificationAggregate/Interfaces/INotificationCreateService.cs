using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Domain.Aggregates.NotificationAggregate.Interfaces
{
    public interface INotificationCreateService
    {
        Task<Result<NotificationMain>> AddNotificationAsync(NotificationCreateDto dto);
    }
}
