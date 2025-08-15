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
    public class MerchantProductController : ControllerBase
    {
        private readonly IMerchantGetProductsService _merchantGetProductService;
        private readonly IMerchantAddProductService _merchantAddProductService;
        private readonly IMerchantUpdateProductService _merchantUpdateProductService;
        private readonly IMerchantRemoveProductService _merchantRemoveProductService;


        public MerchantProductController(IMerchantGetProductsService merchantProductService, IMerchantAddProductService merchantAddProductService, IMerchantUpdateProductService merchantUpdateProductService, IMerchantRemoveProductService merchantRemoveProductService)
        {
            _merchantGetProductService = merchantProductService;
            _merchantAddProductService = merchantAddProductService;
            _merchantUpdateProductService = merchantUpdateProductService;
            _merchantRemoveProductService = merchantRemoveProductService;
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
        public async Task<IActionResult> AddProduct([FromBody] ProductWriteOptions opt)
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

        [HttpPatch("uuid")]
        [AuthorizePermission(RoleName.shop_owner, Permissions.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct(Guid uuid, [FromBody] ProductWriteOptions opt)
        {
            var result = await _merchantUpdateProductService.UpdateProduct(uuid, opt);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("uuid")]
        [AuthorizePermission(RoleName.shop_owner, Permissions.RemoveProduct)]
        public async Task<IActionResult> DeleteProduct(Guid uuid)
        {
            var result = await _merchantRemoveProductService.RemoveProduct(uuid);
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
