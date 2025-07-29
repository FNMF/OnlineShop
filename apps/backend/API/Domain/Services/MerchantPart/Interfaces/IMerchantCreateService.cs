using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.MerchantPart.Interfaces
{
    public interface IMerchantCreateService
    {
        Task<Result<Merchant>> AddMerchantAsync(MerchantCreateDto dto);
    }
}
