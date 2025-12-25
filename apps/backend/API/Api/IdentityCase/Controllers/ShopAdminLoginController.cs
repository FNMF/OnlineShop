using API.Api.IdentityCase.Models;
using API.Api.PlatformCase.Models;
using API.Application.IdentityCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [Route("api/auth/login/shop_admin")]
    [ApiController]
    public class ShopAdminLoginController:ControllerBase
    {
        private readonly IShopAdminLoginService _shopAdminLoginService;

        public ShopAdminLoginController(IShopAdminLoginService shopAdminLoginService)
        {
            _shopAdminLoginService = shopAdminLoginService;
        }
        [HttpPost("token")]
        [Authorize(AuthenticationSchemes = "ExpiredAllowed")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"]
                    .FirstOrDefault()?
                    .Replace("Bearer ", "");
            if (accessToken == null)
            {
                return BadRequest("无效的请求数据");
            }
            var result = await _shopAdminLoginService.RefreshTokenAsync(accessToken, refreshToken);
            if (result.IsSuccess)
            {
                return Ok(result); // 返回刷新成功的结果（比如 Token 等）
            }
            else
            {
                return Unauthorized(result); // 刷新失败的错误信息
            }
        }
        [HttpPost("account")]
        //[AuthorizePermission(Permissions.VerifyAdmin)]
        public async Task<IActionResult> LoginByAccount([FromBody] ShopAdminLoginByAccountOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }

            var result = await _shopAdminLoginService.LoginByAccountAsync(opt);
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
        public async Task<IActionResult> LoginByValidationCode([FromBody] ShopAdminLoginByValidationCodeOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }
            var result = await _shopAdminLoginService.LoginByPhoneAsync(opt);
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
