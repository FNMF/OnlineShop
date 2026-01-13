using API.Common.Helpers;
using API.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.Ops
{
    [Route("api/ops/healthy")]
    [ApiController]
    public class HealthyController:ControllerBase
    {
        private readonly OnlineShopContext _dbContext;
        private readonly JwtHelper _jwtHelper;

        public HealthyController(OnlineShopContext dbContext, JwtHelper jwtHelper)
        {
            _dbContext = dbContext;
            _jwtHelper = jwtHelper;
        }

        // Warining: 这个接口不要暴露在公网，以免被滥用
        [HttpGet("db")]
        public IActionResult PingDb()
        {
            try
            {
                // 尝试执行最简单的查询
                var testQuery = _dbContext.Roles.FirstOrDefault(); // 或 SELECT 1
                return Ok(new
                {
                    dbConnected = true,
                    testResult = testQuery != null ? new { id = testQuery.Id, name = testQuery.Name } : null
                });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                var innerMessages = new List<string>();
                var inner = dbEx.InnerException;
                while (inner != null)
                {
                    innerMessages.Add(inner.Message);
                    inner = inner.InnerException;
                }

                return StatusCode(500, new
                {
                    dbConnected = false,
                    errorType = "DbUpdateException",
                    errorMessage = dbEx.Message,
                    innerErrors = innerMessages,
                    stackTrace = dbEx.StackTrace
                });
            }
            catch (MySql.Data.MySqlClient.MySqlException mySqlEx)
            {
                var innerMessages = new List<string>();
                var inner = mySqlEx.InnerException;
                while (inner != null)
                {
                    innerMessages.Add(inner.Message);
                    inner = inner.InnerException;
                }

                return StatusCode(500, new
                {
                    dbConnected = false,
                    errorType = "MySqlException",
                    errorNumber = mySqlEx.Number,
                    errorMessage = mySqlEx.Message,
                    innerErrors = innerMessages,
                    stackTrace = mySqlEx.StackTrace
                });
            }
            catch (Exception ex)
            {
                var innerMessages = new List<string>();
                var inner = ex.InnerException;
                while (inner != null)
                {
                    innerMessages.Add(inner.Message);
                    inner = inner.InnerException;
                }

                return StatusCode(500, new
                {
                    dbConnected = false,
                    errorType = "Exception",
                    errorMessage = ex.Message,
                    innerErrors = innerMessages,
                    stackTrace = ex.StackTrace
                });
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
