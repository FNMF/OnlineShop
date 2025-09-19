using API.Domain.Enums;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant")]
    public class MerchantOrderController:ControllerBase
    {
        [HttpGet("orders")]
        [Authorize]
        [AuthorizePermission(RoleName.shop_owner, Permissions.GetOrder)]
        public IActionResult GetAllOrders()
        {
            //TODO
            return Ok("Get all orders - Merchant");
        }
    }
}
