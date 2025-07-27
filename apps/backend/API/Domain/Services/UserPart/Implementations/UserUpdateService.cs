using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.AddressPart;
using API.Infrastructure.Repositories;

namespace API.Domain.Services.UserPart.Implementations
{
    public class UserUpdateService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserUpdateService> _logger;

        public UserUpdateService(IUserRepository userRepository, ILogger<UserUpdateService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<User>> UpdateUserAsync(UserUpdateDto dto)
        {
            try
            {
                var result = UserFactory.Update(dto);
                if (!result.IsSuccess)
                {
                    return Result<User>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _userRepository.UpdateUserAsync(result.Data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<User>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
        /*
         * 后续应该有通过事件实现的增加减分的方法，但是目前暂时用不到，实现MVP为重
         */
    }
}
