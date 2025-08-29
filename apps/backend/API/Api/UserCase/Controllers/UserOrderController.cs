using API.Api.Common.Models;
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
        private readonly IUserPutOrderService _userPutOrderService;
        public UserOrderController(IUserGetAllOrdersService userGetAllOrdersService, IUserGetOrderService userGetOrderService, IUserPutOrderService userPutOrderService)
        {
            _userGetAllOrdersService = userGetAllOrdersService;
            _userGetOrderService = userGetOrderService;
            _userPutOrderService = userPutOrderService;
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
        public async Task<IActionResult> CreateOrder(OrderWriteOptions opt)
        {
            var result = await _userPutOrderService.UserPutOrder(opt);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPatch("orders/{orderUuid}")]
        [Authorize]
        //TODO，这个要分状态机，不应该直接Update
        public async Task<IActionResult> UpdateOrder(Guid orderUuid)//Options
        {
            return Ok("UserOrderController");
        }

        
        [HttpDelete("orders/{orderUuid}")]
        [Authorize]
        //TODO,这个可以不实现，订单一般都要留存记录
        public async Task<IActionResult> DeleteOrder(Guid orderUuid)
        {
            return BadRequest("NotAllowed");
        }
    }
}
