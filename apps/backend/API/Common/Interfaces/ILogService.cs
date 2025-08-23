using API.Api.Common.Models;
using API.Domain.Entities.Models;
using API.Domain.Enums;

namespace API.Common.Interfaces
{
    public interface ILogService
    {
        Task<bool> AddLog(LogType type, string description, string detail);
        Task<bool> AddLog(LogType type, string description, string detail, Guid objectuuidBytes);
        Task<bool> AddLog(LogType type, string description, string detail, Guid objectuuidBytes, string datajson);
        Task<List<Log>> GetLogs(LogQueryOptions queryOptions);
        /*Task<List<Log>> GetLog(byte[] uuidBytes);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end);*/

    }
}
