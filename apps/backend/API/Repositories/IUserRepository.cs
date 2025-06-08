using API.Entities.Models;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByOpenIdAsync(string openId);
        Task<User> GetByUuidAsync(byte[] uuidBytes);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}
