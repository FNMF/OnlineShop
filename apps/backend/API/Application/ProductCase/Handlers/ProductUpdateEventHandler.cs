using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.ProductCase;

namespace API.Application.ProductCase.Handlers
{
    public class ProductUpdateEventHandler : IEventHandler<ProductUpdateEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<ProductUpdateEventHandler> _logger;

        public ProductUpdateEventHandler(ILogService logService, ILogger<ProductUpdateEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(ProductUpdateEvent @event, CancellationToken cancellation = default)
        {
            switch (@event.CurrentType)
            {
                case Domain.Enums.CurrentType.Merchant:
                    Console.WriteLine($"Merchant '{@event.AdminUuid}' updated Product {@event.ProductUuid}.");

                    await _logService.AddLog(Domain.Enums.LogType.product, "商户更新商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                case Domain.Enums.CurrentType.Platform:
                    Console.WriteLine($"Platform Admin '{@event.AdminUuid}' updated Product {@event.ProductUuid}.");

                    await _logService.AddLog(Domain.Enums.LogType.product, "平台更新商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                case Domain.Enums.CurrentType.System:
                    Console.WriteLine($"System '{@event.AdminUuid}' updated Product {@event.ProductUuid}.");
                    await _logService.AddLog(Domain.Enums.LogType.product, "系统更新商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                default:
                    Console.WriteLine($"身份错误！'{@event.AdminUuid}' 尝试更新商品");
                    _logger.LogWarning($"身份错误！'{@event.AdminUuid}' 尝试更新商品");
                    break;
            }

        }
    }
}
