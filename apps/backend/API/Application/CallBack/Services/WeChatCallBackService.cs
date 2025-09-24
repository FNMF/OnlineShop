using API.Application.CallBack.Interfaces;
using API.Common.Models.Results;
using API.Infrastructure.WechatPayV3;

namespace API.Application.CallBack.Services
{
    public class WeChatCallBackService: IWeChatCallBackService
    {
        private readonly IWeChatPaymentGateway _weChatPaymentGateway;
        private readonly ILogger<WeChatCallBackService> _logger;
        public WeChatCallBackService(
            IWeChatPaymentGateway weChatPaymentGateway,
            ILogger<WeChatCallBackService> logger)
        {
            _weChatPaymentGateway = weChatPaymentGateway;
            _logger = logger;
        }

        public async Task<Result<WechatTransaction>> HandlePayCallbackAsync(
            string timestamp,
            string nonce,
            string signature,
            string serial,
            string body)
        {
            try
            {
                var result = await _weChatPaymentGateway.HandlePayCallbackAsync(
                    timestamp,
                    nonce,
                    signature,
                    serial,
                    body);
                if(!result.IsSuccess)
                {
                    _logger.LogWarning("处理微信支付回调失败: {Message}", result.Message);
                    return Result<WechatTransaction>.Fail(result.Code, result.Message);
                }
                //TODO,添加业务处理逻辑，比如更新订单状态等
                //Event之类的
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理微信支付回调时发生异常");
                return Result<WechatTransaction>.Fail(ResultCode.ServerError,"处理微信支付回调时发生异常");
            }
        }
    }
}
