using Microsoft.EntityFrameworkCore;
using API.Database;
using API.Entities.Models;

namespace API.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly OnlineshopContext _context;
        public async Task<bool> CreateLog(Log log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes)
        {
            return await _context.Logs.Where(l => l.LogObjectuuid == uuidBytes)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, string type)
        {
            return await _context.Logs.Where(l => l.LogObjectuuid == uuidBytes && l.LogType == type)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end)
        {
            return await _context.Logs.Where(l => l.LogObjectuuid == uuidBytes && l.LogType == type
            && l.LogTime >= start && l.LogTime <= end)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLog(string type, DateTime start, DateTime end)
        {
            return await _context.Logs.Where(l => l.LogType == type
            && l.LogTime >= start && l.LogTime <= end)
                .ToListAsync();
        }
    }
}
