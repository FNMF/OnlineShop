using API.Application.Common.DTOs;
using API.Application.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models;
using API.Domain.Enums;
using API.Domain.Services.Common.Interfaces;
using API.Domain.Services.UserPart.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JwtHelper _jwtHelper;
        private readonly IConfiguration _config;
        private readonly IUserCreateService _userCreateService;
        private readonly IUserReadService _userReadService;
        private readonly ILogService _logService;

        public AuthService(IHttpClientFactory httpClientFactory, JwtHelper jwtHelper, IConfiguration config,IUserCreateService userCreateService, IUserReadService userReadService, ILogService logService)
        {
            _httpClientFactory = httpClientFactory;
            _jwtHelper = jwtHelper;
            _config = config;
            _userCreateService = userCreateService;
            _userReadService = userReadService;
            _logService = logService;
        }

        public async Task<string> LoginWithWxCodeAsync(WxLoginDto dto)
        {
            var appId = _config["WxSettings:AppId"];
            var secret = _config["WxSettings:AppSecret"];

            if (string.IsNullOrWhiteSpace(dto.Code) || string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("登录参数错误");

            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={secret}&js_code={dto.Code}&grant_type=authorization_code";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("请求微信服务器失败");

            var json = await response.Content.ReadAsStringAsync();
            var session = JsonSerializer.Deserialize<WxSessionResult>(json);

            if (session == null || !string.IsNullOrEmpty(session.errmsg))
                throw new Exception($"微信返回错误：{session?.errmsg ?? "未知错误"}");

            var openId = session.openid;
            var sessionKey = session.session_key;

            if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(sessionKey))
                throw new Exception("获取微信 openId 或 sessionKey 失败");
            
            var userInfoJson = DecryptWxData(dto.EncryptedData, dto.Iv, sessionKey);
            var userInfo = JsonSerializer.Deserialize<WxUserInfo>(userInfoJson);

            var phoneInfoJson = DecryptWxData(dto.EncryptedPhoneData, dto.PhoneIv, sessionKey);
            var phoneInfo = JsonSerializer.Deserialize<WxPhoneInfo>(phoneInfoJson);


            var resultofRead = await _userReadService.GetUserByOpenId(session.openid);
            var user = resultofRead.Data;
            if (user == null)
            {
                var userdto = new UserCreateDto(userInfo?.NickName, openId, phoneInfo.PhoneNumber, userInfo.AvatarUrl);
                var resultofCreate = await _userCreateService.AddUserAsync(userdto);
                await _logService.AddLog(LogType.user, "创建新用户", "无", resultofCreate.Data.Uuid, json);
                return _jwtHelper.UserGenerateToken(resultofCreate.Data.OpenId.ToString(), resultofCreate.Data.Uuid , null);
            }

            await _logService.AddLog(LogType.user, "用户登录", "无", user.Uuid, json);
            return _jwtHelper.UserGenerateToken(user.OpenId.ToString(), user.Uuid , null);
        }

        private static string DecryptWxData(string encryptedData, string iv, string sessionKey)
        {
            var encryptedDataBytes = Convert.FromBase64String(encryptedData);
            var ivBytes = Convert.FromBase64String(iv);
            var keyBytes = Convert.FromBase64String(sessionKey);

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            var resultArray = decryptor.TransformFinalBlock(encryptedDataBytes, 0, encryptedDataBytes.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        // 微信 API 返回的 session_key 结构
        public class WxSessionResult
        {
            public string openid { get; set; }
            public string session_key { get; set; }
            public string unionid { get; set; }
            public string errmsg { get; set; }
        }

        // 用户信息
        public class WxUserInfo
        {
            public string NickName { get; set; }
            public string AvatarUrl { get; set; }
        }

        // 手机号信息
        public class WxPhoneInfo
        {
            public string PhoneNumber { get; set; }
        }
    }
}
