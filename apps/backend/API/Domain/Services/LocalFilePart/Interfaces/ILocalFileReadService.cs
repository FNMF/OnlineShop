using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.LocalFilePart.Interfaces
{
    public interface ILocalFileReadService
    {
        Task<Result<Localfile>> GetLocalFileByUuidAsync(Guid uuid);
        Task<Result<Localfile>> GetProductCoverLocalFile(Guid productUuid);
        Task<Result<List<Localfile>>> GetProductDetailLocalFiles(Guid productUuid);
    }
}
