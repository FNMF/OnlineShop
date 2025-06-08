using API.Entities.Dto;
using API.Entities.Models;

namespace API.Services
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddressByUuidAsync(Guid useruuid);
        Task<bool> CreateAddressByUuidAsync(CUAddressDto dto, Guid useruuid);
        Task<bool?> DeleteAddressByUuidAsync(Guid addressuuid);
        Task<bool> UpdateAddressByUuidAsync(Guid addressuuid, CUAddressDto dto);
    }
}
