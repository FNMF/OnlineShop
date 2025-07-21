using API.Common.Models;

namespace API.Common.Interfaces
{
    public interface IRbacService
    {
        Task<bool> HasPermissionAsync(string userId, Permissions permission);
    }
}
