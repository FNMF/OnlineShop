using API.Application.OrderCase.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.PaymentPart.Interfaces;
using API.Infrastructure.WechatPayV3;

namespace API.Domain.Services.PaymentPart.Implementations
{
    public class PaymentCreateService: IPaymentCreateService
    {
        private readonly IPaymentRepsitory _paymentRepository;
        private readonly ILogger<PaymentCreateService> _logger;
        public PaymentCreateService(
            IPaymentRepsitory paymentRepository,
            ILogger<PaymentCreateService> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<Result<Payment>> AddWechatPaymentAsync(
            OrderMain orderMain,
            string appId,
            string mchId,
            string openId
            )
        {
            try
            {
                var payment = new Payment
                {
                    Uuid = UuidV7Helper.NewUuidV7(),
                    Amout = orderMain.OrderTotal,
                    AppId = appId,
                    MchId = mchId,
                    OpenId = openId,
                    OrderUuid = orderMain.OrderUuid,
                    CreatedAt = DateTime.Now,
                    OutTradeNo = orderMain.OrderUuid.ToString("N"),
                    PaymentStatus = "pending"
                };

                if(!await _paymentRepository.AddPaymentAsync(payment))
                {
                    _logger.LogError("创建支付订单时出错");
                    return Result<Payment>.Fail(ResultCode.ServerError, "创建支付订单时出错");
                }
                return Result<Payment>.Success(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Payment>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
