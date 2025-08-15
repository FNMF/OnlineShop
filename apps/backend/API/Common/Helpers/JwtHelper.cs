using API.Common.Models;
using API.Common.Models.Results;
using API.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Common.Helpers
{
    public class JwtHelper
    {
        private readonly JwtSettings _settings;
        private readonly IConfiguration _configuration;

        public JwtHelper(JwtSettings settings, IConfiguration configuration)
        {
            _settings = settings;
            _configuration = configuration;
        }

        public string UserGenerateToken(string? openId, Guid uuid, string? name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uuid.ToString()),
            };
            if (!string.IsNullOrEmpty(openId))          //如果是微信小程序登录会有这个，但是因为如果Claim中有NULL值会报错，所以要拎出来写一个判断，防止报错
            {
                claims.Add(new Claim("OpenId", openId));
            }
            if(!string.IsNullOrEmpty(name))
            {
                claims.Add(new Claim(ClaimTypes.Name, name));           //如果有名字则add，如果没有则无视，理由同上
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_settings.ExpiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string AdminGenerateToken(string Phone, Guid uuid, string account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uuid.ToString()),
                new Claim("Phone", Phone),
                new Claim("Account", account),
            };
             

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_settings.ExpiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //没有验证的解析Token
        public Result AnalysisToken(string token, string type)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims.ToList();
                var anything = claims.FirstOrDefault(c => c.Type == type);
                if (anything == null)
                {
                    return Result.Fail(ResultCode.TokenInvalid,"解析的类别不存在");
                }
                return Result.Success(anything.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResultCode.TokenInvalid, $"Token 解析失败: {ex.Message}");
            }
        }
    }
}
