using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly OnlineshopContext _context;
        public AdminRepository(OnlineshopContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminByAccountAsync(int account)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminAccount == account && a.AdminIsdeleted == false);
        }
        public async Task<Admin> GetAdminByUuidAsync(byte[] uuidBytes)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminUuid == uuidBytes && a.AdminIsdeleted == false);
        }
        public async Task<Admin> GetAdminByPhoneAsync(string phone)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminPhone == phone && a.AdminIsdeleted == false);
        }
        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        public async Task<RoleType> QueryRoleType(Admin admin)
        {
            var roleIds = await _context.Set<AdminRole>()
                .Where(a => a.ArAdminuuid == admin.AdminUuid)
                .Select(a => a.ArRoleid)
                .ToListAsync();

            if (!roleIds.Any())
                return RoleType.none;

            var roleTypes = await _context.Set<Role>()
                .Where(r => roleIds.Contains(r.RoleId))
                .Select(r => r.RoleType)
                .ToListAsync();

            if(roleTypes.Contains(RoleType.system.ToString()))
                return RoleType.system;
            if (roleTypes.Contains(RoleType.platform.ToString()))
                return RoleType.platform;
            if (roleTypes.Contains(RoleType.shop.ToString()))
                return RoleType.shop;

            return RoleType.none;
        }

        public async Task<Result> SetAsNoServiceAdmin(Admin admin)
        {
            var adminRoles = await _context.Set<AdminRole>()
                 .Where(a => a.ArAdminuuid == admin.AdminUuid)
                .ToListAsync();

            if (adminRoles.Any())
            {
                _context.RemoveRange(adminRoles); // 删除原有角色关系
            }

            var noneRole = await _context.Set<Role>()
                .FirstOrDefaultAsync(r => r.RoleName == RoleName.shop_noservice.ToString());

            if (noneRole == null)
            {
                return Result.Fail(ResultCode.NotFound,"未找到对应的角色");
            }
            var newAdminRole = new AdminRole
            {
                ArAdminuuid = admin.AdminUuid,
                ArRoleid = noneRole.RoleId
            };

            await _context.Set<AdminRole>().AddAsync(newAdminRole);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
