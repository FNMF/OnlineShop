using API.Common.Interfaces;
using API.Common.Models;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Common.Middlewares
{
    public class RbacService : IRbacService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RbacService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<bool> HasPermissionAsync(string userId, Permissions permission)
        {
            var userUuidBytes = new Guid(userId).ToByteArray();
            var permissions = await _rolePermissionRepository.GetPermissionsByAdminIdAsync(userUuidBytes);

            return permissions.Any(p => p.PermissionName == permission.ToString());
        }
    }
}
