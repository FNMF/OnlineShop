namespace API.Application.IdentityCase.DTOs
{
    public class AuthResult
    {
        public bool IsNewUser { get; }
        public LoginTokenResult? LoginTokenResult { get; }
        public RegisterTokenResult? RegisterTokenResult { get; }
        public AuthResult(bool isNewUser, LoginTokenResult? loginTokenResult = null, RegisterTokenResult? registerTokenResult = null)
        {
            IsNewUser = isNewUser;
            LoginTokenResult = loginTokenResult;
            RegisterTokenResult = registerTokenResult;
        }
    }
}
