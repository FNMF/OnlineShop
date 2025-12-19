using API.Common.Models;
using API.Common.Models.Results;
using API.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Common.Helpers
{
    public class JwtHelper
    {
        private readonly JwtSettings _settings;

        public JwtHelper(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
            if (_settings.SecretKey == null)
                throw new Exception("JwtSettings.SecretKey is NULL");
        }

        private SigningCredentials GetCredentials()
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.SecretKey));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken CreateToken(IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresMinutes),
                signingCredentials: GetCredentials()
            );
        }
        private JwtSecurityToken CreateShortLivedToken(IEnumerable<Claim> claims, int minutes)
        {
            return new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: GetCredentials()
            );
        }

        public string UserGenerateToken(string? openId, Guid uuid, string? name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uuid.ToString())
            };

            if (!string.IsNullOrEmpty(openId))
            {
                claims.Add(new Claim("OpenId", openId));
            }

            if (!string.IsNullOrEmpty(name))
            {
                claims.Add(new Claim(ClaimTypes.Name, name));
            }

            var token = CreateToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string AdminGenerateToken(string phone, Guid uuid, int account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uuid.ToString()),
                new Claim("Phone", phone),
                new Claim("Account", account.ToString())
            };

            var token = CreateToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // 无验证解析，仅用于非安全场景
        public Result AnalysisToken(string token, string type)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == type);
                if (claim == null)
                {
                    return Result.Fail(ResultCode.TokenInvalid, "解析的类别不存在");
                }

                return Result.Success(claim.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResultCode.TokenInvalid, $"Token 解析失败: {ex.Message}");
            }
        }

        public string GenerateRegisterTempToken(string phone)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.MobilePhone, phone),
                    new Claim("Type", "RegisterTemp")
                };
            var token = CreateShortLivedToken(claims, 5); // 5分钟有效期

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
