using API.Domain.Entities.Models;

namespace API.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByAccountAsync(int account);
        Task<Admin> GetAdminByUuidAsync(byte[] uuidBytes);
        Task<Admin> GetAdminByPhoneAsync(string phone);
        Task<Admin> AddAdminAsync(Admin admin);
        Task<Admin> UpdateAdminAsync(Admin admin);

    }
}
