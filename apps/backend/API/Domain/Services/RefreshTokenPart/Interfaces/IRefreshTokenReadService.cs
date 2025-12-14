using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.RefreshTokenPart.Interfaces
{
    public interface IRefreshTokenReadService
    {
        Task<Result<List<RefreshToken>>> GetRefreshTokenByTargetUuid(Guid targetUuid);
        Task<Result> VerifyToken(Guid targetUuid, string refreshToken);
    }
}
