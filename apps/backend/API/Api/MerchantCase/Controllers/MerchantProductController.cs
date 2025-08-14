using API.Api.Common.Models;
using API.Application.MerchantCase.Interfaces;
using API.Common.Interfaces;
using API.Domain.Enums;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant/products")]
    [ApiController]
    public class MerchantProductController:ControllerBase
    {
        private readonly IMerchantGetProductsService _merchantGetProductService;
        private readonly IMerchantAddProductService _merchantAddProductService;


        public MerchantProductController(IMerchantGetProductsService merchantProductService, IMerchantAddProductService merchantAddProductService = null)
        {
            _merchantGetProductService = merchantProductService;
            _merchantAddProductService = merchantAddProductService;
        }

        [HttpGet("")]
        [AuthorizePermission(RoleName.shop_owner, Permissions.GetProduct)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _merchantGetProductService.GetAllProducts();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("")]
        [AuthorizePermission(RoleName.shop_owner, Permissions.AddProduct)]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateOptions opt)
        {
            var result = await _merchantAddProductService.AddProduct(opt);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /* todo
         * 未完成开发
         * [HttpPatch("uuid")]
        [AuthorizePermission(RoleName.shop_owner, Permissions.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct(Guid uuid, [FromBody] )*/
    }
}
