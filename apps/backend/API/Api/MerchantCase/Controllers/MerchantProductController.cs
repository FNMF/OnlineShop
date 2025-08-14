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
        private readonly ICurrentService _currentService;

        public MerchantProductController(IMerchantGetProductsService merchantProductService, ICurrentService currentService)
        {
            _merchantGetProductService = merchantProductService;
            _currentService = currentService;
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
        public async Task<IActionResult> AddProduct()
        {
            //todo未完成逻辑
            //var result = await _
            return Ok("添加商品的逻辑尚未实现，请稍后再试。");
        }
    }
}
