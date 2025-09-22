using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Entities.Models;

namespace API.Infrastructure.WechatPayV3
{
    public interface IPaymentGateway
    {
        Task<Result<PrepayResult>> CreatePrepayAsync(OrderMain orderMain, string openId);
    }
}
