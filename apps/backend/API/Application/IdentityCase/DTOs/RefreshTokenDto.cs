namespace API.Application.IdentityCase.DTOs
{
    public class RefreshTokenDto
    {
        public string AccessToken { get; }
        public RefreshTokenDto(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
