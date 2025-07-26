using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class ProductReadService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<ProductReadService> _logger;

        public ProductReadService(IAddressRepository addressRepository, ILogger<ProductReadService> logger)
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
