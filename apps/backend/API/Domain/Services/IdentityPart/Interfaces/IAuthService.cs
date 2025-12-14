using API.Application.Common.DTOs;

namespace API.Domain.Services.IdentityPart.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginWithWxCodeAsync(WxLoginDto dto);
    }
}
