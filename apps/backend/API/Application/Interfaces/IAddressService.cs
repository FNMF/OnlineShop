using API.Domain.Entities.Models;
using API.Entities.Dto;

namespace API.Services
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddressByUuidAsync(Guid useruuid);
        Task<bool> CreateAddressByUuidAsync(CURAddressDto dto, Guid useruuid);
        Task<bool?> DeleteAddressByUuidAsync(Guid addressuuid);
        Task<bool> UpdateAddressByUuidAsync(Guid addressuuid, CURAddressDto dto);
    }
}
