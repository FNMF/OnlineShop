using API.Api.UserCase.Models;
using API.Common.Models.Results;
using API.Infrastructure.WechatPayV3;

namespace API.Application.OrderCase.Interfaces
{
    public interface IUserPayOrderService
    {
        Task<Result<WechatPaySignParams>> UserPay(PaymentWriteOptions opt);
    }
}
