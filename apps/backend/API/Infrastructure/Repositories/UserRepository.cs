using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineShopContext _context;

        public UserRepository(OnlineShopContext context)
        {
            _context = context;
        }
        public IQueryable<User> QueryUsers() 
        {
            return _context.Users;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        /*public async Task<User> GetByUuidAsync(byte[] uuidBytes)       //通过uuid查询用户
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Uuid == uuidBytes);

        }

        public async Task<User> GetByOpenIdAsync(string openId)     //通过openid查询用户
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.OpenId == openId);

        }

        public async Task<User> AddUserAsync(User user)        //使用微信传入的openid创建新用户
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUserAsync(User user)      //更新用户，需要EF追踪
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //这里有个删除，但是建议是逻辑删除而不是物理删除
        //所以只需要在service层中修改属性isdeleted然后再update即可
        */
    }
}
