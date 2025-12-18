using API.Api.IdentityCase.Models;
using API.Application.IdentityCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [Route("api/auth/register/merchant")]
    [ApiController]
    public class MerchantRegisterController:ControllerBase
    {
        private readonly IMerchantRegisterService _merchantRegisterService;

        public MerchantRegisterController(IMerchantRegisterService merchantRegisterService)
        {
            _merchantRegisterService = merchantRegisterService;
        }

        [HttpPost("phone")]
        public async Task<IActionResult> RegisterByPhone([FromBody] MerchantRegisterByPhoneOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }

            var result = await _merchantRegisterService.RegisterByPhoneAsync(opt);
            if (result.IsSuccess)
            {
                return Ok(result); 
            }
            else
            {
                return Unauthorized(result); // 登录失败的错误信息
            }
        }

        [HttpPost("temp")]
        [Authorize(AuthenticationSchemes = "RegisterTempToken")]
        public async Task<IActionResult> RegisterByTemp([FromBody] MerchantRegisterByTempOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }
            var result = await _merchantRegisterService.RegisterByTempAsync(opt);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result); // 登录失败的错误信息
            }
        }
    }
}
