using API.Entities.Dto;
using API.Entities.Models;

namespace API.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAddressByUuidAsync(byte[] uuidBytes);
        Task<List<Address>> GetAllIsdefaultAsync(byte[] uuidBytes);
        Task<Address> GetAddressByUuidAsync(byte[] uuidBytes);
        Task<bool> CreateAddressAsync(Address address);
        Task<bool> UpdateAddressAsync(Address address);

    }
}
