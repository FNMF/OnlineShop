using API.Application.Common.EventBus;
using API.Common.Interfaces;
using API.Common.Middlewares;
using API.Domain.Events.PlatformCase;

namespace API.Application.PlatformCase.Handlers
{
    public class PlatformAdminLoginEventHandler : IEventHandler<PlatformAdminLoginEvent>
    {
        private readonly ILogService _logService;
        private readonly ILogger<PlatformAdminLoginEventHandler> _logger;

        public PlatformAdminLoginEventHandler(ILogService logService, ILogger<PlatformAdminLoginEventHandler> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task HandleAsync(PlatformAdminLoginEvent @event, CancellationToken cancellationToken = default)
        {
            // 这里处理事件，例如记录日志
            Console.WriteLine($"User '{@event.PlatformAdminAccount}' logged in.");

            await _logService.AddLog(Domain.Enums.LogType.admin, "平台管理员登录", @event.PlatformAdminAccount.ToString());
            // 如果有其他处理（比如发送消息、记录到数据库等），可以继续处理
            await Task.CompletedTask;
        }
    }
}
