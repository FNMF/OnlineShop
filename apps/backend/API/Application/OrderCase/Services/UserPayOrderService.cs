using API.Api.UserCase.Models;
using API.Application.OrderCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Enums;
using API.Domain.Services.UserPart.Interfaces;
using API.Infrastructure.WechatPayV3;

namespace API.Application.OrderCase.Services
{
    public class UserPayOrderService:IUserPayOrderService
    {
        /// <summary>
        /// 先验证订单与用户的关系
        /// 再验证订单状态是否为待支付
        /// 再调用PrePay接口
        /// 返回支付参数
        /// 前端调用支付SDK
        
        private readonly IOrderReadService _orderReadService;
        private readonly IOrderUpdateService _orderUpdateService;
        private readonly IUserReadService _userReadService;
        private readonly ICurrentService _currentService;
        private readonly IWeChatPaymentGateway _paymentGateway;
        private readonly ILogger<UserPayOrderService> _logger;
        public UserPayOrderService(IOrderReadService orderReadService, IOrderUpdateService orderUpdateService, IUserReadService userReadService, ICurrentService currentService, IWeChatPaymentGateway paymentGateway, ILogger<UserPayOrderService> logger)
        {
            _orderReadService = orderReadService;
            _orderUpdateService = orderUpdateService;
            _userReadService = userReadService;
            _currentService = currentService;
            _paymentGateway = paymentGateway;
            _logger = logger;
        }

        public async Task<Result<WechatPaySignParams>> UserPay(PaymentWriteOptions opt)
        {
            try
            {
                // 1.验证订单与用户的关系
                var orderResult = await _orderReadService.GetOrderByUuid(opt.OrderUuid);
                if (!orderResult.IsSuccess)
                {
                    return Result<WechatPaySignParams>.Fail(orderResult.Code, orderResult.Message);
                }
                if (orderResult.Data.UserUuid != _currentService.RequiredUuid)
                {
                    return Result<WechatPaySignParams>.Fail(ResultCode.Forbidden, "无权操作该订单");
                }

                // 2.验证订单状态是否为待支付
                if (orderResult.Data.OrderStatus != OrderStatus.created.ToString())
                {
                    return Result<WechatPaySignParams>.Fail(ResultCode.InvalidInput, "订单状态不合法");
                }

                // 3.获取用户的OpenId
                var userResult = await _userReadService.GetUserByUuid(_currentService.RequiredUuid);
                if (!userResult.IsSuccess)
                {
                    return Result<WechatPaySignParams>.Fail(userResult.Code, userResult.Message);
                }
                var openId = userResult.Data.OpenId;

                // 4.将Order转化成聚合
                var orderMainResult = OrderFactory.ToAggregate(orderResult.Data);
                if(!orderMainResult.IsSuccess)
                {
                    return Result<WechatPaySignParams>.Fail(orderMainResult.Code, orderMainResult.Message);
                }
                var orderMain = orderMainResult.Data;

                // 5.调用PrePay接口
                var prepayResult = await _paymentGateway.CreatePrepayAsync(orderMain, openId);
                if (!prepayResult.IsSuccess) 
                {
                    return Result<WechatPaySignParams>.Fail(prepayResult.Code, prepayResult.Message);
                }

                return Result<WechatPaySignParams>.Success(prepayResult.Data.SignParams, "获取支付参数成功");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                return Result<WechatPaySignParams>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }

    }
}
