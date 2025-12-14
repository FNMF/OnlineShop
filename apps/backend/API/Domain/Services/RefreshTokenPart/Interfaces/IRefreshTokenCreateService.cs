using API.Common.Models.Results;

namespace API.Domain.Services.RefreshTokenPart.Interfaces
{
    public interface IRefreshTokenCreateService
    {
        Task<Result<String>> AddWeekRefreshTokenAsnyc(Guid targetUuid);
    }
}
