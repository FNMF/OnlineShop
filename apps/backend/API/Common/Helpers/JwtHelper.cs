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

        public ClaimsPrincipal? ValidateAccessTokenIgnoreExpiry(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false, // 无视时间验证信息
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_settings.SecretKey)),
                        ValidIssuer = _settings.Issuer,
                        ValidAudience = _settings.Audience
                    },
                    out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }

    }
}
