using API.Application.OrderCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.UserCase.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/user")]
    public class UserOrderController:ControllerBase
    {
        private readonly IUserGetAllOrdersService _userGetAllOrdersService;
        private readonly IUserGetOrderService _userGetOrderService;
        public UserOrderController(IUserGetAllOrdersService userGetAllOrdersService, IUserGetOrderService userGetOrderService)
        {
            _userGetAllOrdersService = userGetAllOrdersService;
            _userGetOrderService = userGetOrderService;
        }

        [HttpGet("orders")]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _userGetAllOrdersService.GetAllOrders();
            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("orders/{orderUuid}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByUuid(Guid orderUuid)
        {
            var result = await _userGetOrderService.GetOrderByUuid(orderUuid);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("orders")]
        [Authorize]
        //TODO
        public async Task<IActionResult> CreateOrder()//Options
        {
            return Ok("UserOrderController");
        }
        [HttpPatch("orders/{orderUuid}")]
        [Authorize]
        //TODO
        public async Task<IActionResult> UpdateOrder(Guid orderUuid)//Options
        {
            return Ok("UserOrderController");
        }
        [HttpDelete("orders/{orderUuid}")]
        [Authorize]
        //TODO
        public async Task<IActionResult> DeleteOrder(Guid orderUuid)
        {
            return Ok("UserOrderController");
        }
    }
}
