using API.Api.MerchantCase.Models;
using API.Api.PlatformCase.Models;
using API.Application.MerchantCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant/login")]
    [ApiController]
    public class MerchantLoginController:ControllerBase
    {
        private readonly IMerchantLoginService _merchantLoginService;

        public MerchantLoginController(IMerchantLoginService merchantLoginService)
        {
            _merchantLoginService = merchantLoginService;
        }

        [HttpPost("account")]
        //[AuthorizePermission(Permissions.VerifyAdmin)]
        public async Task<IActionResult> LoginByAccount([FromBody] MerchantLoginByAccountOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }

            var result = await _merchantLoginService.LoginByAccountAsync(opt);
            if (result.IsSuccess)
            {
                return Ok(result); // 返回登录成功的结果（比如 Token 等）
            }
            else
            {
                return Unauthorized(result); // 登录失败的错误信息
            }
        }
    }
}
