using API.Application.DTOs;
using API.Domain.Entities.Models;

namespace API.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddressByUuidAsync(Guid useruuid);
        Task<bool> CreateAddressByUuidAsync(CURAddressDto dto, Guid useruuid);
        Task<bool?> DeleteAddressByUuidAsync(Guid addressuuid);
        Task<bool> UpdateAddressByUuidAsync(Guid addressuuid, CURAddressDto dto);
    }
}
