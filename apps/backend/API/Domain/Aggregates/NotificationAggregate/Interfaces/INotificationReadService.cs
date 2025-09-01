using API.Api.Common.Models;
using API.Common.Models.Results;

namespace API.Domain.Aggregates.NotificationAggregate.Interfaces
{
    public interface INotificationReadService
    {
        Task<Result<List<NotificationMain>>> GetAllNotificationsByUuidAsync(NotificationQueryOptions opt);
    }
}
