using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Entities.Models;

namespace API.Domain.Services.PaymentPart.Interfaces
{
    public interface IPaymentCreateService
    {
        Task<Result<Payment>> AddWechatPaymentAsync(
            OrderMain orderMain,
            string appId,
            string mchId,
            string openId
            );
    }
}
