using API.Api.MerchantCase.Models;
using API.Application.MerchantCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant/register")]
    [ApiController]
    public class MechantRegisterController:ControllerBase
    {
        private readonly IMerchantRegisterService _merchantRegisterService;

        public MechantRegisterController(IMerchantRegisterService merchantRegisterService)
        {
            _merchantRegisterService = merchantRegisterService;
        }

        [HttpPost("account")]
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
    }
}
