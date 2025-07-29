using API.Common.Models.Results;

namespace API.Domain.Services.LocalFilePart.Interfaces
{
    public interface ILocalFileRemoveService
    {
        Task<Result> RemoveLocalFileAsync(byte[] localFileUuid);
    }
}
