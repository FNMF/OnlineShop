using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.ValueObjects;

namespace API.Domain.Services.External
{
    public interface IRiderFeeService
    {
        Task<Result<RiderFeeMain>> GetRiderFeeMainAsync(RiderFeeCreateDto dto);
    }
}
