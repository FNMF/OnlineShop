using API.Entities.Dto;

namespace API.Services
{
    public interface IAdminService
    {
        Task<RAdminDto> GetAdminByUuid(Guid uuid);
        Task<RAdminDto> GetAdminByAccount(int account);
        Task<RAdminDto> UpdateAdmin(CUAdminDto dto,Guid uuid);
        Task<bool> DeleteAdminByUuid(Guid uuid);
        Task<RAdminDto> CreateAdmin(CUAdminDto dto);
        Task<RAdminDto> LoginByPhone(string phone);
        Task<RAdminDto> LoginByPwd(int account, string pwd);
        Task<RAdminDto> RetrieveByPhone(string phone, string key);
        Task<RAdminDto> RetrieveByAccount(int account, string key);
    }
}
