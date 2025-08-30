using API.Domain.Aggregates.NotificationAggregate.Interfaces;
using API.Domain.Interfaces;

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
    }
}
