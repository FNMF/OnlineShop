using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.UserPart.Interfaces;

namespace API.Domain.Services.UserPart.Implementations
{
    public class UserCreateService: IUserCreateService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserCreateService> _logger;

        public UserCreateService(IUserRepository userRepository, ILogger<UserCreateService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<User>> AddUserAsync(UserCreateDto dto)
        {
            try
            {
                var result = UserFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<User>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _userRepository.AddUserAsync(result.Data);

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<User>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
