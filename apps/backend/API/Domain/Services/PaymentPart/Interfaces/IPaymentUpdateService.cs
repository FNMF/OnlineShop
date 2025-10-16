using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Infrastructure.WechatPayV3;

namespace API.Domain.Services.PaymentPart.Interfaces
{
    public interface IPaymentUpdateService
    {
        Task<Result<Payment>> MarkAsAccepedNoCommit(WechatTransaction wechatTransaction);
    }
}
