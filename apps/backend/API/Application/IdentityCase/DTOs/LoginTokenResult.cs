using API.Application.Common.DTOs;

namespace API.Application.IdentityCase.DTOs
{
    public class LoginTokenResult
    {
        public string Jwt { get; }
        public string RefreshToken { get; }
        public int ExpiresIn { get; }   // 秒
        public AdminReadDto ReadDto { get; }
        public LoginTokenResult(string jwt, string refreshToken, int expiresIn, AdminReadDto readDto)
        {
            Jwt = jwt;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            ReadDto = readDto;
        }
    }
}
