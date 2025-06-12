using API.Entities.Models;

namespace API.Services
{
    public interface IUserService
    {
        Task<User> GetByOpenIdAsync(string openId);
        Task<User> GetByUuidAsync(Guid useruuid);
        Task<User> CreateUserWithOpenIdAsync(string openId);
        Task<string?> RenameUserByUuidAsync(Guid useruuid, string newName);
        Task<int?> UpdateUserBpByUuidAsync(Guid useruuid, int bp, string detail);
        Task<int?> UpdateUserCreditByUuidAsync(Guid useruuid, int credit,string detail);
        Task<bool> DeleteUserByUuidAsync(Guid useruuid);
    }
}
