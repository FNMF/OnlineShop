using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.IdentityPart.Interfaces
{
    public interface IShopAdminRegisterService
    {
        Task<Result<Admin>> Register(ShopAdminCreateDto dto);
    }
}
