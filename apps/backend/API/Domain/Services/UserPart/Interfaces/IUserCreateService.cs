using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.UserPart.Interfaces
{
    public interface IUserCreateService
    {
        Task<Result<User>> AddUserAsync(UserCreateDto dto);
    }
}
