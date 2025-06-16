using API.Api.Models;
using API.Entities.Models;

namespace API.Services
{
    public interface ILogService
    {
        Task<bool> AddLog(string type, string description, string detail);
        Task<bool> AddLog(string type, string description, string detail, byte[] objectuuidBytes, string datajson);
        Task<List<Log>> GetLogs(LogQueryOptions queryOptions);
        /*Task<List<Log>> GetLog(byte[] uuidBytes);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end);*/

    }
}
