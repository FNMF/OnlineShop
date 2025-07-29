using API.Application.Common.DTOs;

namespace API.Domain.Services.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginWithWxCodeAsync(WxLoginDto dto);
    }
}
