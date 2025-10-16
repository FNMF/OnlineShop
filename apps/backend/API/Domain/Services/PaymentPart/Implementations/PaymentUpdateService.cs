using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.PaymentPart.Interfaces;
using API.Infrastructure.WechatPayV3;

namespace API.Domain.Services.PaymentPart.Implementations
{
    public class PaymentUpdateService: IPaymentUpdateService
    {
        private readonly IPaymentRepsitory _paymentRepository;
        private readonly ILogger<PaymentUpdateService> _logger;
        public PaymentUpdateService(
            IPaymentRepsitory paymentRepository,
            ILogger<PaymentUpdateService> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<Result<Payment>> MarkAsAccepedNoCommit(WechatTransaction wechatTransaction)
        {
            try
            {
                var payment = _paymentRepository
                    .QueryPayments()
                    .FirstOrDefault(p => p.OutTradeNo == wechatTransaction.OutTradeNo);
                if (payment == null)
                {
                    _logger.LogWarning("未找到对应的支付记录, OutTradeNo: {OutTradeNo}", wechatTransaction.OutTradeNo);
                    return Result<Payment>.Fail(ResultCode.NotFound, "未找到对应的支付记录");
                }
                payment.PaymentStatus = "accepted";
                payment.TransactionId = wechatTransaction.TransactionId;
                payment.Currency = wechatTransaction.Amount.Currency;
                payment.TradeType = wechatTransaction.TradeType;
                payment.BankType = wechatTransaction.BankType;
                payment.Attach = wechatTransaction.Attach;
                payment.SuccessTime = wechatTransaction.SuccessTime.LocalDateTime;
                payment.RawCallBackData = System.Text.Json.JsonSerializer.Serialize(wechatTransaction);
                if (!await _paymentRepository.UpdatePaymentAsyncNoCommit(payment))
                {
                    _logger.LogError("更新支付记录时出错, PaymentId: {PaymentId}", payment.OutTradeNo);
                    return Result<Payment>.Fail(ResultCode.ServerError, "更新支付记录时出错");
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
