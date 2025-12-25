using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.MerchantCase;

namespace API.Application.IdentityCase.Handlers
{
    public class ShopAdminRegisterEventHandler:IEventHandler<ShopAdminRegisterEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<ShopAdminRegisterEventHandler> _logger;

        public ShopAdminRegisterEventHandler(ILogService logService, ILogger<ShopAdminRegisterEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(ShopAdminRegisterEvent @event, CancellationToken cancellation = default)
        {
            // 这里处理事件，例如记录日志
            Console.WriteLine($"User '{@event.Phone}' registed.");

            await _logService.AddLog(Domain.Enums.LogType.merchant, "商户管理员注册", @event.Phone);
            // 如果有其他处理（比如发送消息、记录到数据库等），可以继续处理
            await Task.CompletedTask;
        }
    }
}
