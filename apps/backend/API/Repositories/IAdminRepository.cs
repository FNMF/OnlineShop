using API.Entities.Dto;
using API.Entities.Models;

namespace API.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByAccountAsync(int account);
        Task<Admin> GetAdminByUuidAsync(byte[] uuidBytes);
        Task<Admin> GetAdminByPhoneAsync(string phone);
        Task<Admin> CreateAdminAsync(Admin admin);
        Task<Admin> UpdateAdminAsync(Admin admin);
        
    }
}
