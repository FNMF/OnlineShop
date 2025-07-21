using API.Common.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using System.Security.Claims;

namespace API.Common.Middlewares
{
    public class CurrentService : ICurrentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        private ClaimsPrincipal? ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;
        public bool IsAuthenticated => ClaimsPrincipal?.Identity?.IsAuthenticated ?? false;
        public byte[]? CurrentUuid
        {
            get
            {
                var uuidString = GetClaim(ClaimTypes.NameIdentifier);
                return Guid.TryParse(uuidString, out var guid)
                    ? guid.ToByteArray()
                    : null;
            }
        }
        public string? CurrentName => GetClaim(ClaimTypes.Name);
        public CurrentType CurrentType
        {
            get
            {
                var role = GetClaim(ClaimTypes.Role)?.ToLowerInvariant();
                return role switch
                {
                    "platform" => CurrentType.Platform,
                    "merchant" => CurrentType.Merchant,
                    "user" => CurrentType.User,
                    "system" => CurrentType.System,
                    "rider" => CurrentType.Rider,
                    _ => CurrentType.Other
                };
            }
        }
        private string? GetClaim(string claimType) =>
        ClaimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}
