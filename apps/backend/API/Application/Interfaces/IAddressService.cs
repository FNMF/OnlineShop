using API.Api.Models;
using API.Application.DTOs;
using API.Domain.Entities.Models;

namespace API.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddressByUuid(AddressQueryOptions addressQueryOptions);
        Task<bool> CreateAddressByUuid(AddressCreateOptions addressCreateOptions);
        Task<bool?> DeleteAddressByUuid(AddressDeleteOptions addressDeleteOptions);
        Task<bool> UpdateAddressByUuid(AddressUpdateOptions addressUpdateOptions);
    }
}
