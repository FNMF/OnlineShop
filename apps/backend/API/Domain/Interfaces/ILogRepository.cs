using API.Domain.Entities.Models;

namespace API.Repositories
{
    public interface ILogRepository
    {
        Task<bool> AddLog(Log log);
        IQueryable<Log> QueryLog();
        /*Task<List<Log>> GetLog(byte[] uuidBytes, int pageNumber, int pageSize);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, int pageNumber, int pageSize);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end, int pageNumber, int pageSize);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end, int pageNumber, int pageSize);*/
    }
}
