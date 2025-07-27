using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Domain.Services.Common.Interfaces;
using System.Security.Cryptography;

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

        public async Task<Result> VerifyPlatformPasswordAsync(int account, string password)
        {
            try
            {
                var admin = await _repository.GetAdminByAccountAsync(account);

                Console.WriteLine(admin.AdminSalt);
                Console.WriteLine(admin.AdminPwdhash);
                Console.WriteLine(PwdHashHelper.Hashing(password, admin.AdminSalt));

                if (admin == null)
                {
                    _logger.LogWarning("管理员不存在");
                    return Result.Fail(ResultCode.NotExist, "管理员不存在");
                }
                if (await _repository.QueryRoleType(admin) != RoleType.platform)
                {
                    _logger.LogWarning("身份错误");
                    return Result.Fail(ResultCode.NotExist, "身份错误");
                }

                if (CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(admin.AdminPwdhash), Convert.FromBase64String(PwdHashHelper.Hashing(password, admin.AdminSalt))))
                {
                    string jwt = _jwtHelper.GenerateToken(null, new Guid(admin.AdminUuid), account.ToString());
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

        public async Task<Result> VerifyShopPasswordAsync(int account, string password)
        {
            try
            {
                var admin = await _repository.GetAdminByAccountAsync(account);

                Console.WriteLine(admin.AdminSalt);
                Console.WriteLine(admin.AdminPwdhash);
                Console.WriteLine(PwdHashHelper.Hashing(password, admin.AdminSalt));

                if (admin == null)
                {
                    _logger.LogWarning("管理员不存在");
                    return Result.Fail(ResultCode.NotExist, "管理员不存在");
                }
                if (await _repository.QueryRoleType(admin) != RoleType.shop)
                {
                    _logger.LogWarning("身份错误");
                    return Result.Fail(ResultCode.NotExist, "身份错误");
                }

                if (CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(admin.AdminPwdhash), Convert.FromBase64String(PwdHashHelper.Hashing(password, admin.AdminSalt))))
                {
                    string jwt = _jwtHelper.GenerateToken(null, new Guid(admin.AdminUuid), account.ToString());
                    return Result.Success(jwt);
                }
                else
                {
                    return Result.Fail(ResultCode.LoginVerifyError, "用户名或密码错误");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证密码时出错");
                return Result.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
