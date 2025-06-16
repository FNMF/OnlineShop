using API.Domain.Entities.Models;
using API.Repositories;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ILogService _logService;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, ILogService logService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _logService = logService;
        }
        public async Task<User> GetByUuidAsync(Guid useruuid)       //通过uuid查询用户
        {
            try
            {
                byte[] uuidBytes = useruuid.ToByteArray();
                var user = await _userRepository.GetByUuidAsync(uuidBytes);
                if (user == null) { _logger.LogWarning("用户不存在"); }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询用户时出错,UserUuid:{Uuid}", useruuid);
                return null;
            }

        }
        public async Task<User> GetByOpenIdAsync(string openId)     //通过openid查询用户
        {
            try
            {
                var user = await _userRepository.GetByOpenIdAsync(openId);
                if (user == null)
                {
                    _logger.LogWarning("用户不存在");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询用户时出错,OpenId:{OpemId}", openId);
                return null;
            }

        }
        public async Task<User> CreateUserWithOpenIdAsync(string openId)        //使用微信传入的openid创建新用户
        {
            try
            {
                var user = new User { UserOpenid = openId, UserCreatedat = DateTime.Now, UserUuid = Guid.NewGuid().ToByteArray() };
                await _userRepository.AddUserAsync(user);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询用户时出错,OpenId:{OpemId}", openId);
                return null;
            }

        }
        public async Task<string?> RenameUserByUuidAsync(Guid useruuid, string newName)      //使用uuid改名
        {
            try
            {
                byte[] uuidBytes = useruuid.ToByteArray();
                var user = await _userRepository.GetByUuidAsync(uuidBytes);
                if (user == null)
                {
                    _logger.LogWarning("用户不存在");
                    return null;
                }

                user.UserName = newName;
                await _userRepository.UpdateUserAsync(user);
                await _logService.AddLog("user", "用户改名", "无", uuidBytes, newName);

                return newName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "修改名称时出错,UserUuid:{Uuid}", useruuid);
                return null;
            }

        }
        public async Task<int?> UpdateUserBpByUuidAsync(Guid useruuid, int bp, string detail)       //使用uuid更新bp状态（+=）
        {
            try
            {
                byte[] uuidBytes = useruuid.ToByteArray();
                var user = await _userRepository.GetByUuidAsync(uuidBytes);
                if (user == null)
                {
                    _logger.LogWarning("用户不存在");
                    return null;
                }

                user.UserBp += bp;
                await _userRepository.UpdateUserAsync(user);
                await _logService.AddLog("bp", "积分状态更新", detail, uuidBytes, bp.ToString());

                return user.UserBp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新用户积分时出错,UserUuid:{Uuid}", useruuid);
                return null;
            }

        }
        public async Task<int?> UpdateUserCreditByUuidAsync(Guid useruuid, int credit, string detail)       //使用uuid更新credit状态（+=）
        {
            try
            {
                byte[] uuidBytes = useruuid.ToByteArray();
                var user = await _userRepository.GetByUuidAsync(uuidBytes);
                if (user == null)
                {
                    _logger.LogWarning("用户不存在");
                    return null;
                }

                user.UserCredit += credit;
                await _userRepository.UpdateUserAsync(user);
                await _logService.AddLog("credit", "信用状态更新", detail, uuidBytes, credit.ToString());

                return user.UserCredit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新用户信用状态时出错,UserUuid:{Uuid}", useruuid);
                return null;
            }

        }

        public async Task<bool> DeleteUserByUuidAsync(Guid useruuid)        //使用逻辑删除实现删除（防止关联表出错以及后续审计）
        {
            try
            {
                byte[] uuidBytes = useruuid.ToByteArray();
                var user = await _userRepository.GetByUuidAsync(uuidBytes);
                if (user == null)
                {
                    _logger.LogWarning("用户不存在");
                    return false;
                }
                user.UserIsdeleted = true;
                await _userRepository.UpdateUserAsync(user);
                await _logService.AddLog("user", "删除用户", "逻辑删除", uuidBytes, "无");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除用户状态时出错,UserUuid:{Uuid}", useruuid);
                return false;
            }



        }
    }
}
