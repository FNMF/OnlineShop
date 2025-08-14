using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IAdminRoleRepository
    {
        Task<List<Role>> GetRolesByAdminIdAsync(byte[] adminUuid);
    }
}
