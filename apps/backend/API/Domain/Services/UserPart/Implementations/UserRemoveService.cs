using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.UserPart.Interfaces;
using API.Infrastructure.Repositories;
using Sprache;
using Result = API.Common.Models.Results.Result;

namespace API.Domain.Services.UserPart.Implementations
{
    public class UserRemoveService:IUserRemoveService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserRemoveService> _logger;

        public UserRemoveService(IUserRepository userRepository, ILogger<UserRemoveService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveUserAsync(Guid userUuid)
        {
            try
            {
                if (userUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _userRepository.QueryUsers();

                var user = query.FirstOrDefault(a => a.UserUuid == userUuid && a.UserIsdeleted == false);

                if (user == null)
                {
                    return Result.Fail(ResultCode.NotFound, "用户不存在或已删除");
                }

                user.UserIsdeleted = true;
                await _userRepository.UpdateUserAsync(user);

                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
