using API.Application.Common.ProductCase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace API.Api.UserCase.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserProductController: ControllerBase
    {
        private readonly IGetProductService _getProductService;

        public UserProductController(IGetProductService getProductService)
        {
            _getProductService = getProductService;
        }

        [HttpGet("merchants/{merchantUuid}/products")]
        public async Task<IActionResult> GetAllProducts(Guid merchantUuid)
        {
            var result = await _getProductService.GetAllProducts(merchantUuid.ToByteArray());
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("products/{productUuid}")]
        public async Task<IActionResult> GetProductByUuid(Guid productUuid)        //这个是获取单个商品的接口
        {
            var result = await _getProductService.GetProductByUuid(productUuid);
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
