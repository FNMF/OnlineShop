using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.RefreshTokenPart.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;

namespace API.Domain.Services.RefreshTokenPart.Implementations
{
    public class RefreshTokenReadService : IRefreshTokenReadService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<RefreshTokenReadService> _logger;
        public RefreshTokenReadService(IRefreshTokenRepository refreshTokenRepository, ILogger<RefreshTokenReadService> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        public async Task<Result<List<RefreshToken>>> GetRefreshTokenByTargetUuid(Guid targetUuid)
        {
            try
            {
                var token = await _refreshTokenRepository.QueryRefreshTokens()
                    .Where(t => t.TargetUuid == targetUuid && !t.IsRevoked)
                    .OrderByDescending(t => t.ExpiresAt)
                    .ToListAsync();
                if (token.Count == 0)
                {
                    return Result<List<RefreshToken>>.Fail(ResultCode.NotFound, "没有找到相关刷新令牌");
                }

                return Result<List<RefreshToken>>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<RefreshToken>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public async Task<Result> VerifyToken(Guid targetUuid, string refreshToken)
        {
            try
            {
                // 尝试获取对应的刷新令牌
                var tokensResult = await GetRefreshTokenByTargetUuid(targetUuid);
                // 失败则返回
                if (!tokensResult.IsSuccess)
                {
                    return Result.Fail(tokensResult.Code, tokensResult.Message);
                }

                var tokens = tokensResult.Data;
                var tokenHash = Convert.ToBase64String(
                    System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(refreshToken))
                    );
                // 尝试比对
                var matched = tokens.FirstOrDefault(t =>
                    t.Token == tokenHash &&
                    t.ExpiresAt > DateTime.UtcNow
                    );

                if (matched == null)
                {
                    return Result<RefreshToken>.Fail(
                        ResultCode.TokenInvalid, "RefreshToken 无效或已过期");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RefreshToken 验证失败");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
