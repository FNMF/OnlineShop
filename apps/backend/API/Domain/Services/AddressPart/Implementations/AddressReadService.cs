using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class AddressReadService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressReadService> _logger;

        public AddressReadService(IAddressRepository addressRepository, ILogger<AddressReadService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Result<List<Address>>> GetAllAddress()
        {
            return null;
        }
    }
}
