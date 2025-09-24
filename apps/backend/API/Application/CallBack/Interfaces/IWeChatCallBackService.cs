using API.Common.Models.Results;
using API.Infrastructure.WechatPayV3;

namespace API.Application.CallBack.Interfaces
{
    public interface IWeChatCallBackService
    {
        Task<Result<WechatTransaction>> HandlePayCallbackAsync(
            string timestamp,
            string nonce,
            string signature,
            string serial,
            string body);
    }
}
