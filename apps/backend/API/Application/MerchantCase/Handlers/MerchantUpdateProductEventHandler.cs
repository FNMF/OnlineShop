using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.MerchantCase;

namespace API.Application.MerchantCase.Handlers
{
    public class MerchantUpdateProductEventHandler:IEventHandler<MerchantUpdateProductEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<MerchantUpdateProductEventHandler> _logger;

        public MerchantUpdateProductEventHandler(ILogService logService, ILogger<MerchantUpdateProductEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(MerchantUpdateProductEvent @event, CancellationToken cancellation = default)
        {
            Console.WriteLine($"User '{@event.MerchantAdminUuid}' update Product {@event.ProductUuid} .");
            await _logService.AddLog(Domain.Enums.LogType.product, "商户更新商品", @event.MerchantAdminUuid.ToString(), @event.ProductUuid);
        }
    }
}
