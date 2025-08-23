using API.Common.Models.Results;

namespace API.Domain.Services.LocalFilePart.Interfaces
{
    public interface ILocalFileRemoveService
    {
        Task<Result> RemoveLocalFileAsync(Guid localFileUuid);
        Task<Result> RemoveProductAllLocalFilesAsync(Guid productUuid);
    }
}
