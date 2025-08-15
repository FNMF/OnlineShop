using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.MerchantCase;

namespace API.Application.MerchantCase.Handlers
{
    public class MerchantAddProductEventHandler:IEventHandler<MerchantAddProductEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<MerchantAddProductEventHandler> _logger;

        public MerchantAddProductEventHandler(ILogService logService, ILogger<MerchantAddProductEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(MerchantAddProductEvent @event, CancellationToken cancellation = default)
        {
            Console.WriteLine($"User '{@event.MerchantAdminUuid}' add new Product {@event.ProductUuid} .");

            await _logService.AddLog(Domain.Enums.LogType.product, "商户添加商品", @event.MerchantAdminUuid.ToString(), @event.ProductUuid);
        }
    }
}
