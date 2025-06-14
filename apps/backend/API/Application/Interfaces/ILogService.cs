using API.Entities.Models;

namespace API.Services
{
    public interface ILogService
    {
        Task<bool> CreateLog(string type, string description, string detail);
        Task<bool> CreateLog(string type, string description, string detail, byte[] objectuuidBytes, string datajson);
        Task<List<Log>> GetLog(byte[] uuidBytes);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end);

    }
}
