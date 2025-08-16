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

        public async Task<Result<List<Localfile>>> UpdateBatchLocalFilesAsync(List<LocalFileUpdateDto> dtos)
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
                    var result = await _localFileFactory.Update(dto);
                    if (!result.IsSuccess)
                    {
                        // 这里可以选择“全部失败就回滚”，也可以选择“部分成功”
                        // 如果要部分成功，可以把成功的放进 localFiles，最后返回 Success(localFiles)
                        // 追求一致性，要么全部成功要么全部失败
                        return Result<List<Localfile>>.Fail(result.Code, result.Message);
                    }
                    localFiles.Add(result.Data);
                }
                await _localFileRepository.UpdateBatchLocalFilesAsync(localFiles);
                return Result<List<Localfile>>.Success(localFiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<Localfile>>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
        public async Task<Result> MarkAsDeleteAsync(Localfile localfile)
        {
            try
            {
                if (localfile == null || localfile.LocalfileUuid == null)
                {
                    return Result.Fail(ResultCode.InvalidInput, "文件或文件UUID不能为空");
                }
                localfile.LocalfileIsdeleted = true;
                await _localFileRepository.UpdateLocalFileAsync(localfile);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
        public async Task<Result> BatchMarkAsDeleteAsync(List<Localfile> localfiles)
        {
            try
            {
                if(localfiles == null || localfiles.Count == 0)
                {
                    return Result.Fail(ResultCode.InvalidInput, "文件列表不能为空");
                }

                foreach (var file in localfiles)
                {
                    if (file.LocalfileUuid == null)
                    {
                        return Result.Fail(ResultCode.InvalidInput, "文件UUID不能为空");
                    }
                    file.LocalfileIsdeleted = true;
                }
                await _localFileRepository.UpdateBatchLocalFilesAsync(localfiles);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
