using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.RefreshTokenPart.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace API.Domain.Services.RefreshTokenPart.Implementations
{
    public class RefreshTokenCreateService : IRefreshTokenCreateService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<RefreshTokenCreateService> _logger;
        public RefreshTokenCreateService(IRefreshTokenRepository refreshTokenRepository, ILogger<RefreshTokenCreateService> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }
        public async Task<Result<String>> AddWeekRefreshTokenAsnyc(Guid targetUuid)
        {
            try
            {
                // 生成64随机字符串
                var rawToken = Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64)
                    );
                // hash处理
                var tokenHash = Convert.ToBase64String(
                    SHA256.HashData(Encoding.UTF8.GetBytes(rawToken))
                    );
                var refreshToken = new RefreshToken
                {
                    TargetUuid = targetUuid,
                    Token = tokenHash,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),     // 统一UTC，这里是持续一周的刷新令牌
                    IsRevoked = false,
                };
                if (!await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken))
                {
                    return Result<String>.Fail(ResultCode.BusinessError, "创建刷新令牌时出错");
                }
                return Result<String>.Success(rawToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<String>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        // 如果后续有其余时间也可以类似处理
    }
}
