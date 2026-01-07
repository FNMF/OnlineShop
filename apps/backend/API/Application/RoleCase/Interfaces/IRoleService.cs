using API.Common.Models.Results;

namespace API.Application.RoleCase.Interfaces
{
    public interface IRoleService
    {
        Task<Result> GetShopAdminRoleTest();
    }
}
