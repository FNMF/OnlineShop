using API.Domain.Enums;

namespace API.Common.Interfaces
{
    public interface IRbacService
    {
        Task<bool> HasPermissionAsync(string userId, Permissions permission);
        Task<bool> HasRoleAsync(string userId, RoleName role);
    }
}
