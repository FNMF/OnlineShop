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
            var userUuid = new Guid(userId);
            var permissions = await _rolePermissionRepository.GetPermissionsByAdminIdAsync(userUuid);

            return permissions.Any(p => p.Name == permission.ToString());
        }

        public async Task<bool> HasRoleAsync(string userId, RoleName role)
        {
            var userUuid = new Guid(userId);
            var roles = await _adminRoleRepository.GetRolesByAdminIdAsync(userUuid);

            return roles.Any(r => r.Name == role.ToString());
        }
    }
}
