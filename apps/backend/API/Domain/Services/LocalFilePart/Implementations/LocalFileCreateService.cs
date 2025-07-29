using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.LocalFilePart.Interfaces;

namespace API.Domain.Services.LocalFilePart.Implementations
{
    public class LocalFileCreateService:ILocalFileCreateService
    {
        private readonly ILocalFileRepository _localRepository;
        private readonly ILocalFileFactory _localFileFactory;
        private readonly ILogger<LocalFileCreateService> _logger;

        public LocalFileCreateService(ILocalFileRepository localRepository,ILocalFileFactory localFileFactory, ILogger<LocalFileCreateService> logger)
        {
            _localRepository = localRepository;
            _localFileFactory = localFileFactory;
            _logger = logger;
        }

        public async Task<Result<Localfile>> AddLocalFileAsync(LocalFileCreateDto dto)
        {
            try
            {
                var result =  await _localFileFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _localRepository.AddLocalFileAsync(result.Data);

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
