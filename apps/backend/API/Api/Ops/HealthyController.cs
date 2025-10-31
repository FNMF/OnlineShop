using Microsoft.AspNetCore.Mvc;

namespace API.Api.Ops
{
    [Route("api/ops/healthy")]
    [ApiController]
    public class HealthyController:ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
    /*
     * 后面添加各组件健康检查接口
     * 如数据库检测、缓存检测等
     */
}
