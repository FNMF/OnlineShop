using API.Common.Helpers;

namespace API.Common.Middlewares
{
    public class TraceId
    {
        private readonly RequestDelegate _next;

        public TraceId(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var traceId = UuidV7Helper.NewUuidV7().ToString(); 
            context.Items["TraceId"] = traceId;

            // 可添加到响应头（便于前端调试）
            context.Response.Headers["X-Trace-Id"] = traceId;

            await _next(context);
        }
    }
}
