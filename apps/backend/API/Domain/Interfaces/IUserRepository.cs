using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByOpenIdAsync(string openId);
        Task<User> GetByUuidAsync(byte[] uuidBytes);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}
