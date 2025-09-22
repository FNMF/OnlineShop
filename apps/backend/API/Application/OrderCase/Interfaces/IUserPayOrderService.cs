using API.Api.UserCase.Models;
using API.Application.OrderCase.DTOs;
using API.Common.Models.Results;

namespace API.Application.OrderCase.Interfaces
{
    public interface IUserPayOrderService
    {
        Task<Result<WechatPaySignParams>> UserPay(PaymentWriteOptions opt);
    }
}
