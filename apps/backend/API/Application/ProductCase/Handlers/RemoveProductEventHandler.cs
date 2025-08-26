using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.ProductCase;

namespace API.Application.ProductCase.Handlers
{
    public class RemoveProductEventHandler : IEventHandler<RemoveProductEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<RemoveProductEventHandler> _logger;

        public RemoveProductEventHandler(ILogService logService, ILogger<RemoveProductEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(RemoveProductEvent @event, CancellationToken cancellation = default)
        {
            switch (@event.CurrentType)
            {
                case Domain.Enums.CurrentType.Merchant:
                    Console.WriteLine($"Merchant '{@event.AdminUuid}' removed Product {@event.ProductUuid}.");

                    await _logService.AddLog(Domain.Enums.LogType.product, "商户移除商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                case Domain.Enums.CurrentType.Platform:
                    Console.WriteLine($"Platform Admin '{@event.AdminUuid}' removed Product {@event.ProductUuid}.");

                    await _logService.AddLog(Domain.Enums.LogType.product, "平台移除商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                case Domain.Enums.CurrentType.System:
                    Console.WriteLine($"System '{@event.AdminUuid}' removed Product {@event.ProductUuid}.");
                    await _logService.AddLog(Domain.Enums.LogType.product, "系统移除商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                default:
                    Console.WriteLine($"身份错误！'{@event.AdminUuid}' 尝试移除商品");
                    _logger.LogWarning($"身份错误！'{@event.AdminUuid}' 尝试移除商品");
                    break;
            }

        }
    }
}
