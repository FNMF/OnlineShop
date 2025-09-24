using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Entities.Models;

namespace API.Infrastructure.WechatPayV3
{
    public interface IWeChatPaymentGateway
    {
        Task<Result<PrepayResult>> CreatePrepayAsync(OrderMain orderMain, string openId);
        Task<Result<WechatTransaction>> HandlePayCallbackAsync(
        string timestamp,
        string nonce,
        string signature,
        string serial,
        string body);
    }
}
