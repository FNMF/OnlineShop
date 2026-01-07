using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IAdminRoleRepository
    {
        Task<bool> MarkAsAdmin(Guid adminUuid);
        Task<List<Role>> GetRolesByAdminIdAsync(Guid adminUuid);
    }
}
