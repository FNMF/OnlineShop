using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Domain.Services.AdminPart.Interfaces
{
    public interface IShopAdminReadService
    {
        Task<Result<AdminReadDto>> GetAdminByUuid(Guid uuid);
        Task<Result<AdminReadDto>> GetAdminByPhone(String phone);
    }
}
