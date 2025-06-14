namespace API.Services
{
    public interface IAuthService
    {
        Task<string> LoginWithWxCodeAsync(string code);
    }
}
