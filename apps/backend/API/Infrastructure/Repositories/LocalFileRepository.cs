using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class LocalFileRepository: ILocalFileRepository
    {
        public readonly OnlineShopContext _context;
        public LocalFileRepository(OnlineShopContext context)
        {
            _context = context;
        }
        public async Task<bool> AddLocalFileAsync(Localfile localFile)
        {
            await _context.AddAsync(localFile);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddBatchLocalFilesAsync(List<Localfile> localFiles)
        {
            await _context.AddRangeAsync(localFiles);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateLocalFileAsync(Localfile localfile)
        {
            _context.Update(localfile);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateBatchLocalFilesAsync(List<Localfile> localfiles)
        {
            _context.UpdateRange(localfiles);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveLocalFileAsync(Localfile localfile)
        {
            _context.Remove(localfile);
            await _context.SaveChangesAsync();
            return true;
        }
        public IQueryable<Localfile> QueryLocalFiles()
        {
            return _context.Localfiles;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
