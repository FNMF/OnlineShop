using API.Api.PlatformCase.Models;
using API.Application.PlatformCase.Interfaces;
using API.Common.Models;
using API.Domain.Entities.Models;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.PlatformCase.Controllers
{
    [Route("api/Platform/[controller]")]
    [ApiController]
    public class PlatformAdminLoginController:ControllerBase
    {
        private readonly IPlatformAdminLoginService _platformAdminLoginService;

        public PlatformAdminLoginController(IPlatformAdminLoginService platformAdminLoginService)
        {
            _platformAdminLoginService = platformAdminLoginService;
        }


        /// <summary>
        /// 平台管理员通过账号密码登录
        /// </summary>
        /// <param name="loginDto">登录DTO</param>
        /// <returns>登录结果</returns>
        [HttpPost("account")]
        //[AuthorizePermission(Permissions.VerifyAdmin)]
        public async Task<IActionResult> LoginByAccount([FromBody] PlatformAdminLoginByAccountDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("无效的请求数据");
            }

            var result = await _platformAdminLoginService.LoginByAccountAsync(loginDto);
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
