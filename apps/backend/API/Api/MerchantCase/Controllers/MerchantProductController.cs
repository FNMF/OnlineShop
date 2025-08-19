using API.Api.Common.Models;
using API.Application.Common.ProductCase.Interfaces;
using API.Domain.Enums;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant/products")]
    [ApiController]
    public class MerchantProductController : ControllerBase
    {
        private readonly IGetProductService _getProductService;
        private readonly IAddProductService _addProductService;
        private readonly IUpdateProductService _updateProductService;
        private readonly IRemoveProductService _removeProductService;


        public MerchantProductController(IGetProductService productService, IAddProductService addProductService, IUpdateProductService updateProductService, IRemoveProductService removeProductService)
        {
            _getProductService = productService;
            _addProductService = addProductService;
            _updateProductService = updateProductService;
            _removeProductService = removeProductService;
        }

        [HttpGet("")]
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner, Permissions.GetProduct)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _getProductService.GetAllProducts();
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
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner, Permissions.AddProduct)]
        public async Task<IActionResult> AddProduct([FromBody] ProductWriteOptions opt)
        {
            var result = await _addProductService.AddProduct(opt);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("{uuid}")]
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner,Permissions.GetProduct)]
        public async Task<IActionResult> GetProductByUuid(Guid uuid)        //这个是获取单个商品的接口
        {
            var result = await _getProductService.GetProductByUuid(uuid);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("{uuid}")]
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner, Permissions.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct(Guid uuid, [FromBody] ProductWriteOptions opt)
        {
            var result = await _updateProductService.UpdateProduct(uuid, opt);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{uuid}")]
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner, Permissions.RemoveProduct)]
        public async Task<IActionResult> DeleteProduct(Guid uuid)
        {
            var result = await _removeProductService.RemoveProduct(uuid);
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
