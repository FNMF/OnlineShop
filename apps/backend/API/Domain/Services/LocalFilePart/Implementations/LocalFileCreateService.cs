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

        public async Task<Result<List<Localfile>>> AddBatchLocalFilesAsync(List<LocalFileCreateDto> dtos)
        {
            try
            {
                if (dtos == null || dtos.Count == 0)
                {
                    return Result<List<Localfile>>.Fail(ResultCode.InvalidInput, "文件列表不能为空");
                }

                var localFiles = new List<Localfile>();

                foreach (var dto in dtos)
                {
                    var result = await _localFileFactory.Create(dto);
                    if (!result.IsSuccess)
                    {
                        // 这里可以选择“全部失败就回滚”，也可以选择“部分成功”
                        // 如果要部分成功，可以把成功的放进 localFiles，最后返回 Success(localFiles)
                        // 但是一般推荐要么全成功要么全失败，保持一致性
                        return Result<List<Localfile>>.Fail(result.Code, result.Message);
                    }

                    localFiles.Add(result.Data);
                }

                await _localRepository.AddBatchLocalFilesAsync(localFiles);

                return Result<List<Localfile>>.Success(localFiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<Localfile>>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
