using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<List<Permission>> GetPermissionsByAdminIdAsync(byte[] adminUuid);
    }
}
