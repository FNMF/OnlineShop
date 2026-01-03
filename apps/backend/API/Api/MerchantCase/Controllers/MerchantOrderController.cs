using API.Application.OrderCase.Interfaces;
using API.Domain.Enums;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant")]
    public class MerchantOrderController:ControllerBase
    {
        private readonly IMerchantGetAllOrdersService _merchantGetAllOrdersService;
        public MerchantOrderController(IMerchantGetAllOrdersService merchantGetAllOrdersService)
        {
            _merchantGetAllOrdersService = merchantGetAllOrdersService;
        }
        [HttpGet("orders")]
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner, Permissions.GetOrder)]
        public async Task<IActionResult> GetAllOrders()
        {
           var result = await _merchantGetAllOrdersService.GetAllOrders();
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
