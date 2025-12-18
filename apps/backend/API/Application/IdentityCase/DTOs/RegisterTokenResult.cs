namespace API.Application.IdentityCase.DTOs
{
    public class RegisterTokenResult
    {
        public string TempToken { get; }
        public int ExpiresIn { get; }   // 秒
        public RegisterTokenResult(string tempToken, int expiresIn)
        {
            TempToken = tempToken;
            ExpiresIn = expiresIn;
        }
    }
}
