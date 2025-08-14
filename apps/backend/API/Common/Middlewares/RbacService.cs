using API.Common.Interfaces;
using API.Domain.Enums;
using API.Domain.Interfaces;

namespace API.Common.Middlewares
{
    public class RbacService : IRbacService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IAdminRoleRepository _adminRoleRepository;

        public RbacService(IRolePermissionRepository rolePermissionRepository, IAdminRoleRepository adminRoleRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _adminRoleRepository = adminRoleRepository;
        }

        public async Task<bool> HasPermissionAsync(string userId, Permissions permission)
        {
            var userUuidBytes = new Guid(userId).ToByteArray();
            var permissions = await _rolePermissionRepository.GetPermissionsByAdminIdAsync(userUuidBytes);

            return permissions.Any(p => p.PermissionName == permission.ToString());
        }

        public async Task<bool> HasRoleAsync(string userId, RoleName role)
        {
            var userUuidBytes = new Guid(userId).ToByteArray();
            var roles = await _adminRoleRepository.GetRolesByAdminIdAsync(userUuidBytes);

            return roles.Any(r => r.RoleName == role.ToString());
        }
    }
}
