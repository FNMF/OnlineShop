using API.Application.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models;
using API.Domain.Enums;
using API.Domain.Services.Common.Interfaces;
using System.Text.Json;

namespace API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        private readonly JwtHelper _jwtHelper;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;
        private readonly ILogService _logService;

        public AuthService(IHttpClientFactory httpClientFactory, IUserService userService, JwtHelper jwtHelper, IConfiguration config, ILogger<AuthService> logger, ILogService logService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
            _jwtHelper = jwtHelper;
            _config = config;
            _logger = logger;
            _logService = logService;
        }

        public async Task<string> LoginWithWxCodeAsync(string code)
        {
            var appId = _config["WxSettings:AppId"];
            var secret = _config["WxSettings:AppSecret"];

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("登录参数错误");

            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={secret}&js_code={code}&grant_type=authorization_code";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("请求微信服务器失败");

            var json = await response.Content.ReadAsStringAsync();
            var session = JsonSerializer.Deserialize<WxSessionResult>(json);

            if (session == null || !string.IsNullOrEmpty(session.errmsg))
                throw new Exception($"微信返回错误：{session?.errmsg ?? "未知错误"}");

            if (string.IsNullOrEmpty(session.openid))
                throw new Exception("获取微信 OpenID 失败");
            //var session = new WxSessionResult { openid = "aZ3kLm8QwP1xVrT9yB2nCfE7HgMd" };//测试用跳过微信给openid


            var user = await _userService.GetByOpenIdAsync(session.openid);
            if (user == null)
            {
                user = await _userService.CreateUserWithOpenIdAsync(session.openid);
                await _logService.AddLog(LogType.user, "创建新用户", "无", user.UserUuid.ToArray(), json);
                return _jwtHelper.GenerateToken(user.UserOpenid.ToString(), new Guid(user.UserUuid), CurrentType.User, null);
            }

            await _logService.AddLog(LogType.user, "用户登录", "无", user.UserUuid.ToArray(), json);
            return _jwtHelper.GenerateToken(user.UserOpenid.ToString(), new Guid(user.UserUuid), CurrentType.User, null);
        }

    }
}
