using API.Common.Models;
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

        public JwtHelper(JwtSettings settings)
        {
            _settings = settings;
        }

        public string GenerateToken(string? openId, Guid uuid, CurrentType userRole, string? name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uuid.ToString()),
                new Claim(ClaimTypes.Role, userRole.ToString())
            };
            if (!string.IsNullOrEmpty(openId))          //如果是微信小程序登录会有这个，但是因为如果Claim中有NULL值会报错，所以要拎出来写一个判断，防止报错
            {
                claims.Add(new Claim("OpenId", openId));
            }
            if(!string.IsNullOrEmpty(name))
            {
                claims.Add(new Claim(ClaimTypes.Name, name));           //如果有名字则add，如果没有则无视，理由同上
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
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
    }
}
