using API.Domain.Events;

namespace API.Application.Common.EventBus
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Type>> _handlerTypes = new();
        private readonly IServiceProvider _serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            AutoRegisterHandlers();
        }

        private void AutoRegisterHandlers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var type in assemblies.SelectMany(x => x.GetTypes()))
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

                foreach (var handlerInterface in interfaces)
                {
                    var eventType = handlerInterface.GetGenericArguments()[0];

                    if (!_handlerTypes.ContainsKey(eventType))
                        _handlerTypes[eventType] = new List<Type>();

                    _handlerTypes[eventType].Add(type);
                }
            }
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IDomainEvent
        {
            if (!_handlerTypes.TryGetValue(typeof(TEvent), out var handlers)) return;

            foreach (var handlerType in handlers)
            {
                var handler = (IEventHandler<TEvent>?)_serviceProvider.GetService(handlerType);
                if (handler != null)
                {
                    await handler.HandleAsync(@event, cancellationToken);
                }
            }
        }
    }

}
