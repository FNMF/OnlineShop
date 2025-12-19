using API.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.Ops
{
    [Route("api/ops/healthy")]
    [ApiController]
    public class HealthyController:ControllerBase
    {
        private readonly OnlineshopContext _dbContext;

        public HealthyController(OnlineshopContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Warining: 这个接口不要暴露在公网，以免被滥用
        [HttpGet("db")]
        public IActionResult PingDb()
        {
            try
            {
                // 尝试执行简单查询
                var canConnect = _dbContext.Database.CanConnect();
                return Ok(new { dbConnected = canConnect });
            }
            catch (Exception ex)
            {
                // 不要泄露敏感信息
                return StatusCode(500, new { dbConnected = false, error = ex.Message });
            }
        }
        [HttpGet("api")]
        public IActionResult PingApi()
        {
            return Ok();
        }
    }
    /*
     * 后面添加各组件健康检查接口
     * 如数据库检测、缓存检测等
     */
}
