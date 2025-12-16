using API.Api.IdentityCase.Models;
using API.Api.PlatformCase.Models;
using API.Application.IdentityCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [Route("api/auth/login/merchant")]
    [ApiController]
    public class MerchantLoginController:ControllerBase
    {
        private readonly IMerchantLoginService _merchantLoginService;

        public MerchantLoginController(IMerchantLoginService merchantLoginService)
        {
            _merchantLoginService = merchantLoginService;
        }
        [HttpPost("token")]
        [Authorize(AuthenticationSchemes = "ExpiredAllowed")]
        public async Task<IActionResult> LoginByToken([FromBody] string refreshToken)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"]
                    .FirstOrDefault()?
                    .Replace("Bearer ", "");
            if (accessToken == null)
            {
                return BadRequest("无效的请求数据");
            }
            var result = await _merchantLoginService.LoginByTokenAsync(accessToken, refreshToken);
            if (result.IsSuccess)
            {
                return Ok(result); // 返回登录成功的结果（比如 Token 等）
            }
            else
            {
                return Unauthorized(result); // 登录失败的错误信息
            }
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

        [HttpPost("phone")]
        public async Task<IActionResult> LoginByValidationCode([FromBody] MerchantLoginByValidationCodeOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }
            var result = await _merchantLoginService.LoginByPhoneAsync(opt);
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
