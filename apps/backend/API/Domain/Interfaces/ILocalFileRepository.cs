using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface ILocalFileRepository
    {
        Task<bool> AddLocalFileAsync(Localfile localFile);
        Task<bool> AddBatchLocalFilesAsync(List<Localfile> localFiles);
        Task<bool> UpdateLocalFileAsync(Localfile localfile);
        Task<bool> UpdateBatchLocalFilesAsync(List<Localfile> localfiles);
        Task<bool> RemoveLocalFileAsync(Localfile localfile);
        IQueryable<Localfile> QueryLocalFiles();
        Task SaveChangesAsync();
    }
}
