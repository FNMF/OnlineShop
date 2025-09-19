using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.UserPart.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.UserPart.Implementations
{
    public class UserReadService:IUserReadService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserReadService> _logger;

        public UserReadService(IUserRepository userRepository, ILogger<UserReadService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<User>> GetUserByOpenId(string openId)
        {
            try
            {
                var query = _userRepository.QueryUsers();

                var user = await query
                    .FirstOrDefaultAsync(u => u.OpenId == openId);

                if (user == null)
                {
                    return Result<User>.Fail(ResultCode.NotFound, "用户不存在或已删除");
                }

                var result = Result<User>.Success(user);

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<User>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
        /*
         *
         *后续应该有对于Platform统计信息用的条件查询之类的，暂时用不到
         *
         */
    }
}
