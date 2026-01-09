using API.Common.Models.Results;

namespace API.Application.RoleCase.Interfaces
{
    public interface IRoleService
    {
        Task<Result> ApplyShopAdminRoleTest();
        Task<Result<List<string>>> GetRoles();
    }
}
