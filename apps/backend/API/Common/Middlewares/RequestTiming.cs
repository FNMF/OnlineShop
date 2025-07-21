using System.Diagnostics;

namespace API.Common.Middlewares
{
    public class RequestTiming
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTiming> _logger;

        public RequestTiming(RequestDelegate next, ILogger<RequestTiming> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // 让请求继续往下执行
            await _next(context);

            stopwatch.Stop();
            long elapsedMs = stopwatch.ElapsedMilliseconds;

            // 将耗时存入 HttpContext.Items 中
            context.Items["ElapsedMilliseconds"] = elapsedMs;

            // 也可以记录日志
            _logger.LogInformation("请求 {Path} 耗时 {ElapsedMilliseconds} ms", context.Request.Path, elapsedMs);
        }
    }
}
