using API.Domain.Events;

namespace API.Application.Common.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)where TEvent : IDomainEvent;
    }
}
