using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Domain.Services.Common.Interfaces;

namespace API.Domain.Services.Common.Implementations
{
    public class AdminPasswordVerifyService : IAdminPasswordVerifyService
    {
        private readonly IAdminRepository _repository;
        private readonly JwtHelper _jwtHelper;
        private readonly ILogger<AdminPasswordVerifyService> _logger;

        public AdminPasswordVerifyService(IAdminRepository repository, JwtHelper jwtHelper, ILogger<AdminPasswordVerifyService> logger)
        {
            _repository = repository;
            _jwtHelper = jwtHelper;
            _logger = logger;
        }

        public async Task<Result> VerifyPasswordAsync(int account, string password)
        {
            try
            {
                var admin = await _repository.GetAdminByAccountAsync(account);
                if (admin == null)
                {
                    _logger.LogWarning("管理员不存在");
                    return Result.Fail(ResultCode.NotExist, "管理员不存在");
                }
                if (admin.AdminPwdhash == PwdHashHelper.Hashing(password, admin.AdminSalt))
                {
                    string jwt = _jwtHelper.GenerateToken(null, new Guid(admin.AdminUuid), CurrentType.Platform, account.ToString());
                    return Result.Success(jwt);
                }
                else
                {
                    return Result.Fail(ResultCode.LoginVerifyError, "用户名或密码错误");
                }
            }catch (Exception ex)
            {
                _logger.LogError(ex, "验证密码时出错");
                return Result.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
