using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AddressPart.Interfaces
{
    public interface IAddressCreateService
    {
        Task<Result<Address>> AddAddressAsync(AddressCreateDto dto);
    }
}
