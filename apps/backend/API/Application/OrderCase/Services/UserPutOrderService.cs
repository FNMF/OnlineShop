using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.Interfaces;
using API.Application.OrderCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregate.OrderEvents;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Enums;
using API.Domain.Services.AddressPart.Interfaces;
using API.Domain.Services.External;
using API.Domain.Services.MerchantPart.Interfaces;
using API.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace API.Application.OrderCase.Services
{
    public class UserPutOrderService:IUserPutOrderService
    {
        private readonly IOrderCreateService _orderCreateService;
        private readonly IOrderDomainService _orderDomainService;
        private readonly ICartReadService _cartReadService;
        private readonly IMerchantReadService _merchantReadService;
        private readonly IAddressReadService _addressReadService;
        private readonly IRiderFeeService _riderFeeService;
        private readonly ICurrentService _currentService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<UserPutOrderService> _logger;

        public UserPutOrderService(IOrderCreateService orderCreateService, IOrderDomainService orderDomainService, ICartReadService cartReadService, IAddressReadService addressReadService,IMerchantReadService merchantReadService, IRiderFeeService riderFeeService, ICurrentService currentService, IEventBus eventBus, ILogger<UserPutOrderService> logger)
        {
            _orderCreateService = orderCreateService;
            _orderDomainService = orderDomainService;
            _cartReadService = cartReadService;
            _merchantReadService = merchantReadService;
            _addressReadService = addressReadService;
            _riderFeeService = riderFeeService;
            _currentService = currentService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<OrderMain>> UserPutOrder(OrderWriteOptions opt)
        {
            try
            {
                var cartResult =await _cartReadService.GetCartByUuid(opt.CartUuid);
                if (!cartResult.IsSuccess)
                {
                    _logger.LogWarning("没有找到相关购物车");
                    return Result<OrderMain>.Fail(cartResult.Code, cartResult.Message);
                }
                if(cartResult.Data.CartUseruuid != _currentService.RequiredUuid)
                {
                    _logger.LogWarning("没有找到相关购物车");
                    return Result<OrderMain>.Fail(ResultCode.NotFound, "没有找到相关购物车");
                }
                var cartMain = CartFactory.ToAggregate(cartResult.Data).Data;

                var addressResult = await _addressReadService.GetAddress(opt.AddressUuid);
                if (!addressResult.IsSuccess)
                {
                    _logger.LogWarning("没有找到相关地址");
                    return Result<OrderMain>.Fail(addressResult.Code, addressResult.Message);
                }
                if(addressResult.Data.AddressUseruuid != _currentService.RequiredUuid)
                {
                    _logger.LogWarning("没有找到相关地址");
                    return Result<OrderMain>.Fail(ResultCode.NotFound, "没有找到相关地址");
                }
                var merchantResult = await _merchantReadService.GetMerchantByUuidAsync(cartResult.Data.CartMerchantuuid);
                var orderUuid = UuidV7Helper.NewUuidV7();
                var merchantAddress = merchantResult.Data.MerchantCity + merchantResult.Data.MerchantDistrict + AESHelper.Decrypt(merchantResult.Data.MerchantDetail);
                var userAddress = addressResult.Data.AddressCity + addressResult.Data.AddressDistrict + addressResult.Data.AddressDetail;
                //TODO，这里需要添加优惠券服务、支付服务等
                //CouponService

                var riderFeeResult = await _riderFeeService.GetRiderFeeMainAsync(new RiderFeeCreateDto(_currentService.RequiredUuid, orderUuid, opt.AddressUuid, merchantResult.Data.MerchantUuid, userAddress, merchantAddress, opt.ExpectedTime, opt.RiderService));

                //PaymentService

                var orderTotal = cartMain.GetTotalPrice() + cartMain.GetTotalPackingFee() +riderFeeResult.Data.Amount; 
                var orderCreateDto = new OrderMainCreateDto(
                    orderUuid,
                    _currentService.RequiredUuid,
                    orderTotal,
                    OrderStatus.created,
                    "订单未接收",
                    DateTime.Now,
                    merchantAddress,
                    userAddress,
                    orderTotal, /*-CouponDiscount*/
                    cartMain.GetTotalPackingFee(),
                    riderFeeResult.Data.Amount,
                    opt.RiderService,
                    opt.ExpectedTime,
                    opt.Note
                    );

                var orderMainResult = OrderFactory.CreateOrderMain(orderCreateDto);
                if (!orderMainResult.IsSuccess)
                {
                    return Result<OrderMain>.Fail(orderMainResult.Code, orderMainResult.Message);
                }
                var secOrderMainResult = await _orderDomainService.AddItems(orderMainResult.Data, cartMain);
                if (!secOrderMainResult.IsSuccess) 
                {
                    return Result<OrderMain>.Fail(secOrderMainResult.Code, secOrderMainResult.Message);
                }

                var orderCreateResult = await _orderCreateService.CreateOrder(secOrderMainResult.Data);
                if(!orderCreateResult.IsSuccess)
                {
                    return Result<OrderMain>.Fail(orderCreateResult.Code, orderCreateResult.Message);
                }
                // 发布订单创建事件
                await _eventBus.PublishAsync(new OrderCreateEvent(orderCreateResult.Data, merchantResult.Data.MerchantUuid));

                return Result<OrderMain>.Success(orderCreateResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<OrderMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
