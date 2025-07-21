using API.Api.PlatformCase.Models;
using API.Common.Models.Results;

namespace API.Application.PlatformCase.Interfaces
{
    public interface IPlatformAdminLoginService
    {
        Task<Result> LoginByAccountAsync(PlatformAdminLoginByAccountDTO loginDto);
    }
}
