using API.Application.MerchantCase.Interfaces;
using API.Common.Interfaces;
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
        //todo
        //这里需要添加我写的那个身份验证服务
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
        //todo
        //身份验证
        public async Task<IActionResult> AddProduct()
        {
            //var result = await _
            return Ok("添加商品的逻辑尚未实现，请稍后再试。");
        }
    }
}
