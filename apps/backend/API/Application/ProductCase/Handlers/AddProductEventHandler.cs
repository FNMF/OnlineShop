using API.Application.Common.EventBus;
using API.Application.MerchantCase.Handlers;
using API.Common.Interfaces;
using API.Domain.Events.MerchantCase;
using API.Domain.Events.ProductCase;

namespace API.Application.ProductCase.Handlers
{
    public class AddProductEventHandler : IEventHandler<AddProductEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<AddProductEventHandler> _logger;

        public AddProductEventHandler(ILogService logService, ILogger<AddProductEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(AddProductEvent @event, CancellationToken cancellation = default)
        {
            switch (@event.CurrentType)
            {
                case Domain.Enums.CurrentType.Merchant:
                    Console.WriteLine($"Merchant '{@event.AdminUuid}' added new Product {@event.ProductUuid}.");

                    await _logService.AddLog(Domain.Enums.LogType.product, "商户添加商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                case Domain.Enums.CurrentType.Platform:
                    Console.WriteLine($"Platform Admin '{@event.AdminUuid}' added new Product {@event.ProductUuid}.");

                    await _logService.AddLog(Domain.Enums.LogType.product, "平台添加商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                case Domain.Enums.CurrentType.System:
                    Console.WriteLine($"System '{@event.AdminUuid}' added new Product {@event.ProductUuid}.");
                    await _logService.AddLog(Domain.Enums.LogType.product, "系统添加商品", @event.AdminUuid.ToString(), @event.ProductUuid);
                    break;

                default:
                    Console.WriteLine($"身份错误！'{@event.AdminUuid}' 尝试添加商品");
                    _logger.LogWarning($"身份错误！'{@event.AdminUuid}' 尝试添加商品");
                    break;
            }

        }
    }
}
