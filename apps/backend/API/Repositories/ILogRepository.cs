using API.Entities.Models;

namespace API.Repositories
{
    public interface ILogRepository
    {
        Task<bool> CreateLog(Log log);
        Task<List<Log>> GetLog(byte[] uuidBytes);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end);
    }
}
