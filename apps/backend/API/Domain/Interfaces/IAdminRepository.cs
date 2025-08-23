using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;

namespace API.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByAccountAsync(int account);
        Task<Admin> GetAdminByUuidAsync(Guid uuid);
        Task<Admin> GetAdminByPhoneAsync(string phone);
        Task<Admin> AddAdminAsync(Admin admin);
        Task<Admin> UpdateAdminAsync(Admin admin);
        Task<RoleType> QueryRoleType(Admin admin);
        Task<Result> SetAsNoServiceAdmin(Admin admin);

    }
}
