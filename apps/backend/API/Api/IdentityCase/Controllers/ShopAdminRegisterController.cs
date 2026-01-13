using API.Api.IdentityCase.Models;
using API.Application.IdentityCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Api.IdentityCase.Controllers
{
    [Route("api/auth/register/shop_admin")]
    [ApiController]
    public class ShopAdminRegisterController:ControllerBase
    {
        private readonly IShopAdminRegisterService _shopAdminRegisterService;
        private readonly ILogger<ShopAdminRegisterController> _logger;

        public ShopAdminRegisterController(IShopAdminRegisterService merchantRegisterService, ILogger<ShopAdminRegisterController> logger)
        {
            _shopAdminRegisterService = merchantRegisterService;
            _logger = logger;
        }

        [HttpPost("phone")]
        public async Task<IActionResult> RegisterByPhone([FromBody] ShopAdminRegisterByPhoneOptions opt)
        {
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }

            var result = await _shopAdminRegisterService.RegisterByPhoneAsync(opt);
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
        public async Task<IActionResult> RegisterByTemp([FromBody] ShopAdminRegisterByTempOptions opt)
        {
            Request.EnableBuffering();

            Request.Body.Position = 0;
            using var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true);
            var rawBody = await reader.ReadToEndAsync();
            Request.Body.Position = 0;

            _logger.LogInformation("=== RAW REQUEST ===");
            _logger.LogInformation("Content-Type: {ct}", Request.ContentType);
            _logger.LogInformation("Headers: {headers}", Request.Headers);
            _logger.LogInformation("RawBody: {body}", rawBody);
            _logger.LogInformation("Model is null? {isNull}", opt == null);
            if (opt == null)
            {
                return BadRequest("无效的请求数据");
            }
            var result = await _shopAdminRegisterService.RegisterByTempAsync(opt);
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
