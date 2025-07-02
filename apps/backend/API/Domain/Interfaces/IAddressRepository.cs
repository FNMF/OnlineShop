using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IAddressRepository
    {
        IQueryable<Address> QueryAddresses();
        /*Task<List<Address>> GetAllAddressByUuidAsync(byte[] uuidBytes);
        Task<List<Address>> GetAllIsdefaultAsync(byte[] uuidBytes);
        Task<Address> GetAddressByUuidAsync(byte[] uuidBytes);*/
        Task<bool> AddAddressAsync(Address address);
        Task<bool> UpdateAddressAsync(Address address);

    }
}
