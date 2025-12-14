using API.Application.Common.DTOs;
using API.Domain.Services.IdentityPart.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [ApiController]
    [Route("api/auth/user")]
    public class WxAuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public WxAuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("wxlogin")]
        public async Task<IActionResult> WxLogin([FromBody] WxLoginDto dto)
        {
            try
            {
                var token = await _authService.LoginWithWxCodeAsync(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

}
