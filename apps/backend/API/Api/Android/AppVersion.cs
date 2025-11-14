using Microsoft.AspNetCore.Mvc;

namespace API.Api.Android
{
    [Route("api/android/appversion")]
    [ApiController]
    public class AppVersion:ControllerBase
    {
        [HttpGet("latest")]
        public IActionResult actionResult()
        {
            //TODO, 这里后续需要对接真实的版本信息数据源
            var appVersionInfo = new
            {
                versionCode = 0,
                version = "0.0.0",
                downloadUrl = "https://example.com/download/app.apk",
                releaseNotes = "Test"
            };
            return Ok(appVersionInfo);
        }
    }
}
