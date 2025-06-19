using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<bool> AddLogAsync(Log log);
        IQueryable<Log> QueryLogs();
        /*Task<List<Log>> GetLog(byte[] uuidBytes, int pageNumber, int pageSize);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, int pageNumber, int pageSize);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end, int pageNumber, int pageSize);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end, int pageNumber, int pageSize);*/
    }
}
