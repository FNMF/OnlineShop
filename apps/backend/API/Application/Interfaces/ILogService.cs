using API.Api.Models;
using API.Domain.Entities.Models;
using API.Domain.Enums;

namespace API.Application.Interfaces
{
    public interface ILogService
    {
        Task<bool> AddLog(LogType type, string description, string detail);
        Task<bool> AddLog(LogType type, string description, string detail, byte[] objectuuidBytes);
        Task<bool> AddLog(LogType type, string description, string detail, byte[] objectuuidBytes, string datajson);
        Task<List<Log>> GetLogs(LogQueryOptions queryOptions);
        /*Task<List<Log>> GetLog(byte[] uuidBytes);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type);
        Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end);
        Task<List<Log>> GetLog(string type, DateTime start, DateTime end);*/

    }
}
