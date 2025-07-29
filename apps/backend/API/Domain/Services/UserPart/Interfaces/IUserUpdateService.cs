using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.UserPart.Interfaces
{
    public interface IUserUpdateService
    {
        Task<Result<User>> UpdateUserAsync(UserUpdateDto dto);
    }
}
