using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Services.LocalFilePart.Implementations
{
    public class LocalFileReadService
    {
        private readonly ILocalFileRepository _localFileRepository;
        private readonly ILogger<LocalFileReadService> _logger;

        public LocalFileReadService(ILocalFileRepository localFileRepository, ILogger<LocalFileReadService> logger)
        {
            _localFileRepository = localFileRepository;
            _logger = logger;
        }

        //直接通过本地文件的UUID获取本地文件
        public async Task<Result<Localfile>> GetLocalFileByUuidAsync(Guid uuid)
        {
            try
            {
                var localFile = _localFileRepository.QueryLocalFiles().FirstOrDefault(p => p.LocalfileUuid ==uuid.ToByteArray()&&p.LocalfileIsdeleted ==false);
                if (localFile == null)
                {
                    return Result<Localfile>.Fail(ResultCode.NotFound, "文件未找到");
                }
                return Result<Localfile>.Success(localFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Localfile>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }

        //通过商品的UUID获取商品封面本地文件
        public async Task<Result<Localfile>> GetProductCoverLocalFile(Guid productUuid)
        {
            try
            {
                var localFile = _localFileRepository.QueryLocalFiles().FirstOrDefault(p => p.LocalfileObjectuuid == productUuid.ToByteArray() && p.LocalfileType == Enums.LocalfileObjectType.product_cover.ToString() && p.LocalfileIsdeleted == false);
                if (localFile == null)
                {
                    return Result<Localfile>.Fail(ResultCode.NotFound, "文件未找到");
                }
                return Result<Localfile>.Success(localFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Localfile>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }

        //通过商品的UUID获取商品的所有详情文件
        public async Task<Result<List<Localfile>>> GetProductDetailLocalFiles(Guid productUuid)
        {
            try
            {
                var localFiles = _localFileRepository.QueryLocalFiles()
                    .Where(p => p.LocalfileObjectuuid == productUuid.ToByteArray() && p.LocalfileType == Enums.LocalfileObjectType.product_detail.ToString() && p.LocalfileIsdeleted == false)
                    .ToList();
                if (localFiles == null || !localFiles.Any())
                {
                    return Result<List<Localfile>>.Fail(ResultCode.NotFound, "文件未找到");
                }
                return Result<List<Localfile>>.Success(localFiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<Localfile>>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }

        //todo
        //各种其他的查询
    }
}
