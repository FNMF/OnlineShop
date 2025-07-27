using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.UserPart.Interfaces
{
    public interface IUserReadService
    {
        Task<Result<User>> GetUserByOpenId(string openId);
    }
}
