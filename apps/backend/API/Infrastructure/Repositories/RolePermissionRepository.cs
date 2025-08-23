using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class RolePermissionRepository:IRolePermissionRepository
    {
        private readonly OnlineshopContext _context;

        public RolePermissionRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public async Task<List<Permission>> GetPermissionsByAdminIdAsync(Guid adminUuid)
        {
            var permissions = await _context.AdminRoles
                .Where(ar => ar.ArAdminuuid == adminUuid) // 先查出 Admin-Roles 关系
                .Join(
                    _context.Roles, // 连接 Roles 表
                    ar => ar.ArRoleid,
                    r => r.RoleId,
            (ar, r) => r
            )
            .Join(
                    _context.RolePermissions, // 连接 Role-Permissions 表
                    r => r.RoleId,
                    rp => rp.RpRoleid,
                    (r, rp) => rp
            )
            .Join(
                    _context.Permissions, // 连接 Permissions 表
                    rp => rp.RpPermissionid,
                    p => p.PermissionId,
                    (rp, p) => p // 返回 Permission 实体
                )
                .Distinct() // 去重，因为一个 Admin 可能有多个 Role 和多个 Permission
                .ToListAsync();

            return permissions;
        }
    }
}
