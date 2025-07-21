using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface ILocalFileRepository
    {
        Task<bool> AddLocalFile(Localfile localFile);
        Task<bool> AddBatchLocalFile(List<Localfile> localFiles);
        Task<bool> UpdateLocalFile(Localfile localfile);
        Task<bool> RemoveLocalFile(Localfile localfile);
        IQueryable<Localfile> QueryLocalFiles();
        Task SaveChangesAsync();
    }
}
