using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.MerchantPart.Interfaces
{
    public interface IMerchantUpdateService
    {
        Task<Result<Merchant>> UpdateMerchantAsync(MerchantUpdateDto dto);
    }
}
