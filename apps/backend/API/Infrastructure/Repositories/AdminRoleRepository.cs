using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class AdminRoleRepository : IAdminRoleRepository
    {
        private readonly OnlineshopContext _context;

        public AdminRoleRepository(OnlineshopContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRolesByAdminIdAsync(Guid adminUuid)
        {
            var roles = await _context.AdminRoles
                .Where(ar => ar.ArAdminuuid == adminUuid) // 根据 adminUuid 查找 Admin-Roles 关系
                .Join(
                    _context.Roles, // 连接 Roles 表
                    ar => ar.ArRoleid,
                    r => r.RoleId,
                    (ar, r) => r // 返回 Role 实体
                )
                .Distinct() // 去重，确保每个角色只出现一次
                .ToListAsync();

            return roles;
        }
    }
}
