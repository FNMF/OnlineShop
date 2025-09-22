using API.Application.OrderCase.DTOs;

namespace API.Infrastructure.WechatPayV3
{
    public class PrepayResult
    {
        public string PrepayId { get; set; }
        public WechatPaySignParams SignParams { get; set; }
    }
}
