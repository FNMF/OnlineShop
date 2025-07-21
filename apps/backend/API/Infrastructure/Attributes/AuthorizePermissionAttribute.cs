using API.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using API.Domain.Entities.Models;
using API.Common.Models;

namespace API.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
    {
        public Permissions Permission { get; }

        public AuthorizePermissionAttribute(Permissions permission)
        {
            Permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 获取Authorization header中的JWT
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var jwtHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = jwtHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // 使用解析后的 userId 和 role 调用 RBAC 服务判断权限
                var permissionService = context.HttpContext.RequestServices.GetService<IRbacService>();
                if (!permissionService.HasPermissionAsync(userId, Permission).Result)
                {
                    context.Result = new ForbidResult();
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
