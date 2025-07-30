using API.Common.Interfaces;

namespace API.Common.Middlewares
{
    public class ClientIpService: IClientIpService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientIpService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClientIp()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return "Unknown";

            // 优先取 X-Forwarded-For
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',').First().Trim(); // 取第一个 IP
            }

            // 退回 RemoteIpAddress
            return context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown";
        }
    }
}
