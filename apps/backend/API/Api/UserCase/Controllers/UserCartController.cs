
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
        private readonly IUpdateCartService _updateCartService;
        private readonly IRemoveCartService _removeCartService;

        public UserCartController(IGetCartService readCartService, IAddCartService addCartService, IUpdateCartService updateCartService, IRemoveCartService removeCartService)
        {
            _getCartService = readCartService;
            _addCartService = addCartService;
            _updateCartService = updateCartService;
            _removeCartService = removeCartService;
        }
        //获取用户在某个商户的购物车
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
        //创建用户在某个商户的首个购物车
        [HttpPost("merchants/{merchantUuid}/carts/items")]
        [Authorize]
        public async Task<IActionResult> AddCart(Guid merchantUuid, [FromBody] CartWriteOptions opt)
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
        //修改购物车中的商品数量
        [HttpPatch("merchants/{merchantUuid}/carts/items/{productUuid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCart(Guid merchantUuid,Guid productUuid, [FromBody] CartWriteOptions.CartItemWriteOptions opt)
        {
            //TODO
            var result = await _updateCartService.UpdateCartAsync(new CartWriteOptions(merchantUuid, new List<CartWriteOptions.CartItemWriteOptions> { opt }));
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete("merchants/{merchantUuid}/carts/items/{productUuid}")]
        [Authorize]
        public async Task<IActionResult> RemoveCartItem(Guid merchantUuid, Guid productUuid)
        {
            //TODO
            var result = await _removeCartService.RemoveCartItemAsync(merchantUuid, productUuid);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //删除用户在某个商户的购物车
        [HttpDelete("merchants/{merchantUuid}/carts")]
        [Authorize]
        public async Task<IActionResult> RemoveCart(Guid merchantUuid)
        {
           var result = await _removeCartService.RemoveCartAsync(merchantUuid);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
