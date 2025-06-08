using Microsoft.AspNetCore.Mvc;
using API.Entities.Dto;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
                var token = await _authService.LoginWithWxCodeAsync(dto.Code);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

}
