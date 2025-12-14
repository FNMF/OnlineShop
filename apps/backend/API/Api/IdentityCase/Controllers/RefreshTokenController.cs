using API.Application.IdentityCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class RefreshTokenController:ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }
        [HttpPost("refresh")]
        [Authorize(AuthenticationSchemes = "ExpiredAllowed")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"]
                    .FirstOrDefault()?
                    .Replace("Bearer ", "");
            if (accessToken == null)
            {
                return BadRequest("无效的请求数据");
            }

            var result = await _refreshTokenService.RefreshTokenAsync(refreshToken);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);

            }
        }
    }
}
