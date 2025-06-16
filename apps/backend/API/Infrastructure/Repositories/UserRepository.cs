using API.Domain.Entities.Models;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineshopContext _context;

        public UserRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public async Task<User> GetByUuidAsync(byte[] uuidBytes)       //通过uuid查询用户
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserUuid == uuidBytes);

        }

        public async Task<User> GetByOpenIdAsync(string openId)     //通过openid查询用户
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserOpenid == openId);

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
    }
}
