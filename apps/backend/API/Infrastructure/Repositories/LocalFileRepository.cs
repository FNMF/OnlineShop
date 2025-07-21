using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class LocalFileRepository: ILocalFileRepository
    {
        public readonly OnlineshopContext _context;
        public LocalFileRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public async Task<bool> AddLocalFile(Localfile localFile)
        {
            await _context.AddAsync(localFile);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddBatchLocalFile(List<Localfile> localFiles)
        {
            await _context.AddRangeAsync(localFiles);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateLocalFile(Localfile localfile)
        {
            _context.Update(localfile);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveLocalFile(Localfile localfile)
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
