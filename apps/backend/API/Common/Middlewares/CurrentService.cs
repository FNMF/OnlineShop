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
        public Guid? CurrentUuid
        {
            get
            {
                var uuidString = GetClaim(ClaimTypes.NameIdentifier);
                Guid.TryParse(uuidString, out var guid);
                return guid;
            }
        }
        public Guid RequiredUuid
        {
            get
            {
                var uuid = CurrentUuid;
                if (uuid == null)
                {
                    throw new UnauthorizedAccessException("Current UUID is required but not found.");
                }
                return uuid.Value;
            }
        }
        public string? CurrentName => GetClaim(ClaimTypes.Name);
        public string? CurrentPhone => GetClaim(ClaimTypes.MobilePhone);
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
