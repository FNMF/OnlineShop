using API.Api.PlatformCase.Models;
using API.Application.Common.EventBus;
using API.Application.PlatformCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Events.PlatformCase;
using API.Domain.Services.Common.Interfaces;

namespace API.Application.PlatformCase.Services
{
    public class PlatformAdminLoginService : IPlatformAdminLoginService
    {
        private readonly IAdminPasswordVerifyService _adminPasswordVerifyService;
        private readonly EventBus _eventBus;
        private readonly ILogger<PlatformAdminLoginService> _logger;

        public PlatformAdminLoginService(IAdminPasswordVerifyService adminPasswordVerifyService, EventBus eventBus, ILogger<PlatformAdminLoginService> logger)
        {
            _adminPasswordVerifyService = adminPasswordVerifyService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result> LoginByAccountAsync(PlatformAdminLoginByAccountDTO loginDto)
        {
            try
            {
                // 1. 调用 Domain 层的服务来验证账号和密码
                var isValid = await _adminPasswordVerifyService.VerifyPasswordAsync(loginDto.Account, loginDto.Password);

                if (!isValid.IsSuccess)
                {
                    // 2. 密码验证失败，返回错误信息
                    return Result.Fail(ResultCode.LoginVerifyError, "用户名或密码错误");
                }

                // 3. 登录成功，生成一些登录后的业务操作，比如生成 Token 或者事件处理
                // 例如，你可以通过 EventPublisher 触发一些事件
                await _eventBus.PublishAsync(new PlatformAdminLoginEvent(loginDto.Account));

                return Result.Success("登录成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, ex.Message);
            }

        }
    }
}
