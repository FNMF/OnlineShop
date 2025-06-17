namespace API.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginWithWxCodeAsync(string code);
    }
}
