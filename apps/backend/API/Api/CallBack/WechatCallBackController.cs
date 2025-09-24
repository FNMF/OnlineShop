using API.Application.CallBack.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.CallBack
{
    [Route("api/payment/callback")]
    [ApiController]
    public class WechatCallBackController:ControllerBase
    {
        private readonly IWeChatCallBackService _weChatCallBackService;
        public WechatCallBackController(IWeChatCallBackService weChatCallBackService)
        {
            _weChatCallBackService = weChatCallBackService;
        }
        [HttpPost("wechat")]
        public async Task<IActionResult> WechatPayCallBack()
        {
            var timestamp = Request.Headers["Wechatpay-Timestamp"].ToString();
            var nonce = Request.Headers["Wechatpay-Nonce"].ToString();
            var signature = Request.Headers["Wechatpay-Signature"].ToString();
            var serial = Request.Headers["Wechatpay-Serial"].ToString();
            var body = await new StreamReader(Request.Body).ReadToEndAsync();

            var result = await _weChatCallBackService.HandlePayCallbackAsync(
                timestamp,
                nonce,
                signature,
                serial,
                body);
            if (!result.IsSuccess)
            {
                return StatusCode(500, new { code = "FAIL", message = result.Message });
            }
            else
            {
                return Ok();
            }
        }
    }
}
