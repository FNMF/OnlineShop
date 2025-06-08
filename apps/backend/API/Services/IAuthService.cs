using API.Entities.Models;

namespace API.Services
{
    public interface IAuthService
    {
        Task<string> LoginWithWxCodeAsync(string code);
    }
}
