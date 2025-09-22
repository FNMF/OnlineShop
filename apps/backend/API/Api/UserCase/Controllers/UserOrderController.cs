using API.Api.Common.Models;
using API.Api.UserCase.Models;
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
        private readonly IUserPayOrderService _userPayOrderService;
        public UserOrderController(IUserGetAllOrdersService userGetAllOrdersService, IUserGetOrderService userGetOrderService, IUserPutOrderService userPutOrderService, IUserPayOrderService userPayOrderService)
        {
            _userGetAllOrdersService = userGetAllOrdersService;
            _userGetOrderService = userGetOrderService;
            _userPutOrderService = userPutOrderService;
            _userPayOrderService = userPayOrderService;
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
        public async Task<IActionResult> CreateOrder([FromBody] OrderWriteOptions opt)
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
        /// <summary>
        /// 用户发起预支付，返回预支付参数
        /// 需要第三方回调才修改订单信息等
        /// </summary>
        /// <param name="orderUuid"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        [HttpPost("orders/{orderUuid}/payment")]
        [Authorize]
        public async Task<IActionResult> CreatePayment(Guid orderUuid, [FromBody] PaymentWriteOptions opt)
        {
            var result = await _userPayOrderService.UserPay(opt);
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
