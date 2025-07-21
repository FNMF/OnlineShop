using API.Application.Common.EventBus;
using System.Reflection;

namespace API.Infrastructure.Events
{
    public static class EventHandlerRegistrationExtensions
    {
        public static void AddEventHandlers(this IServiceCollection services, Assembly[] assemblies)
        {
            var handlerInterfaceType = typeof(IEventHandler<>);

            var handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType),
                    (type, iface) => new { Implementation = type, Interface = iface });

            foreach (var handler in handlerTypes)
            {
                services.AddScoped(handler.Interface, handler.Implementation);
            }
        }
    }
}
