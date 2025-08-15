using API.Common.Models.Results;
using API.Domain.Interfaces;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Infrastructure.Repositories;

namespace API.Domain.Services.LocalFilePart.Implementations
{
    public class LocalFileRemoveService : ILocalFileRemoveService
    {
        private readonly ILocalFileRepository _localFileRepository;
        private readonly ILogger<LocalFileRemoveService> _logger;

        public LocalFileRemoveService(ILocalFileRepository localFileRepository, ILogger<LocalFileRemoveService> logger)
        {
            _localFileRepository = localFileRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveLocalFileAsync(byte[] localFileUuid)
        {
            try
            {
                if (localFileUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _localFileRepository.QueryLocalFiles();

                var localFile = query.FirstOrDefault(a => a.LocalfileUuid == localFileUuid && a.LocalfileIsdeleted == false);

                if (localFile == null)
                {
                    return Result.Fail(ResultCode.NotFound, "商品不存在或已删除");
                }

                localFile.LocalfileIsdeleted = true;
                await _localFileRepository.UpdateLocalFileAsync(localFile);

                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }

        public async Task<Result> RemoveProductAllLocalFilesAsync(byte[] productUuid)
        {
            try
            {
                if (productUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }
                var query = _localFileRepository.QueryLocalFiles();
                var localFiles = query.Where(a => a.LocalfileObjectuuid == productUuid && a.LocalfileIsdeleted == false).ToList();
                if (!localFiles.Any())
                {
                    return Result.Fail(ResultCode.NotFound, "商品不存在或已删除");
                }
                foreach (var localFile in localFiles)
                {
                    localFile.LocalfileIsdeleted = true;
                    await _localFileRepository.UpdateLocalFileAsync(localFile);
                }
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
