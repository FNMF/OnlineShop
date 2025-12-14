namespace API.Application.MerchantCase.DTOs
{
    public class TokenDto
    {
        public string Jwt { get; }
        public string RefreshToken { get; }
        public int ExpiresIn { get; }   // 秒
        public TokenDto(string jwt, string refreshToken, int expiresIn)
        {
            Jwt = jwt;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
        }
    }
}
