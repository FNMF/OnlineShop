using API.Database;
using API.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly OnlineshopContext _context;
        public async Task<bool> AddLog(Log log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, int pageNumber, int pageSize)
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
        }
    }
}
