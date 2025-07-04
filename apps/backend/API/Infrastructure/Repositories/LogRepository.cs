using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly OnlineshopContext _context;
        public LogRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public async Task<bool> AddLogAsync(Log log)
        {
            await _context.Logs.AddAsync(log);
            int affected = await _context.SaveChangesAsync();       //如果数据库操作大于0则返回true
            return affected > 0;
        }
        public IQueryable<Log> QueryLogs()        //对于Repository层只需要返回IQueryable即可，剩下操作全在Service层完成
        {
            return _context.Logs;
        }
        /*public async Task<List<Log>> GetLog(byte[] uuidBytes, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Logs.Where(l => l.LogObjectuuid == uuidBytes)
                .OrderByDescending(l => l.LogTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, string type, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Logs.Where(l => l.LogObjectuuid == uuidBytes && l.LogType == type)
                .OrderByDescending(l => l.LogTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Logs.Where(l => l.LogObjectuuid == uuidBytes && l.LogType == type
            && l.LogTime >= start && l.LogTime <= end)
                .OrderByDescending(l => l.LogTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLog(string type, DateTime start, DateTime end, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Logs.Where(l => l.LogType == type
            && l.LogTime >= start && l.LogTime <= end)
                .OrderByDescending(l => l.LogTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }*/
    }
}
