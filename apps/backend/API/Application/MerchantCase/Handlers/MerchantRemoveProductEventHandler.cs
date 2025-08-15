using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.MerchantCase;

namespace API.Application.MerchantCase.Handlers
{
    public class MerchantRemoveProductEventHandler:IEventHandler<MerchantRemoveProductEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<MerchantRemoveProductEventHandler> _logger;

        public MerchantRemoveProductEventHandler(ILogService logService, ILogger<MerchantRemoveProductEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(MerchantRemoveProductEvent @event, CancellationToken cancellation = default)
        {
            Console.WriteLine($"User '{@event.MerchantAdminUuid}' remove Product {@event.ProductUuid} .");

            await _logService.AddLog(Domain.Enums.LogType.product, "商户删除商品", @event.MerchantAdminUuid.ToString(), @event.ProductUuid);
        }
    }
}
