using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AddressPart.Interfaces
{
    public interface IAddressReadService
    {
        Task<Result<List<Address>>> GetAllAddresses(AddressQueryOptions opt);
        Task<Result<Address>> GetAddress(Guid addressUuid);
    }
}
