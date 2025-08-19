
using API.Api.Common.Models;
using API.Application.Common.CartCase.Interfaces;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.UserCase.Controllers
{
    [Route("api/user")]
    public class UserCartController:ControllerBase
    {
        private readonly IGetCartService _getCartService;
        private readonly IAddCartService _addCartService;

        public UserCartController(IGetCartService readCartService, IAddCartService addCartService)
        {
            _getCartService = readCartService;
            _addCartService = addCartService;
        }

        [HttpGet("merchants/{merchantUuid}/carts")]
        [Authorize]
        public async Task<IActionResult> GetAllCarts(Guid merchantUuid)
        {
            var result = await _getCartService.GetCartAsync(merchantUuid);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("merchants/products/{productUuid}")]
        [Authorize]
        public async Task<IActionResult> AddCart(Guid productUuid, [FromBody] CartWriteOptions opt)
        {
            var result = await _addCartService.AddCartAsync(opt);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
