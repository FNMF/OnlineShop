namespace API.Domain.Services.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginWithWxCodeAsync(string code);
    }
}
