using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Domain.Events.MerchantCase;

namespace API.Application.MerchantCase.Handlers
{
    public class MerchantLoginEventHandler:IEventHandler<MerchantLoginEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<MerchantLoginEventHandler> _logger;

        public MerchantLoginEventHandler(ILogService logService, ILogger<MerchantLoginEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(MerchantLoginEvent @event,CancellationToken cancellation = default)
        {
            // 这里处理事件，例如记录日志
            Console.WriteLine($"User '{@event.MerchantAdminUuid}' logged in.");

            await _logService.AddLog(Domain.Enums.LogType.merchant, "商户管理员登录",@event.OccurredOn.ToShortTimeString(), @event.MerchantAdminUuid);
            // 如果有其他处理（比如发送消息、记录到数据库等），可以继续处理
            await Task.CompletedTask;
        }
    }
}
