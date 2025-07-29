using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart;
using API.Infrastructure.Repositories;

namespace API.Domain.Services.LocalFilePart.Implementations
{
    public class LocalFileUpdateService: ILocalFileUpdateService
    {
        private readonly ILocalFileRepository _localFileRepository;
        private readonly ILocalFileFactory _localFileFactory;
        private readonly ILogger<LocalFileUpdateService> _logger;

        public LocalFileUpdateService(ILocalFileRepository localFileRepository,ILocalFileFactory localFileFactory, ILogger<LocalFileUpdateService> logger)
        {
            _localFileRepository = localFileRepository;
            _localFileFactory = localFileFactory;
            _logger = logger;
        }

        public async Task<Result<Localfile>> UpdateLocalFileAsync(LocalFileUpdateDto dto)
        {
            try
            {
                var result =await _localFileFactory.Update(dto);
                if (!result.IsSuccess)
                {
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _localFileRepository.UpdateLocalFileAsync(result.Data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Localfile>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
