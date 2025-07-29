using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> QueryUsers();
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        /*Task<User> GetByOpenIdAsync(string openId);
        Task<User> GetByUuidAsync(byte[] uuidBytes);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);*/
    }
}
