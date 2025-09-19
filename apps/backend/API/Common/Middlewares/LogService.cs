using API.Api.Common.Models;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Middlewares
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogger<LogService> _logger;

        public LogService(ILogRepository logRepository, ILogger<LogService> logger)
        {
            _logRepository = logRepository;
            _logger = logger;
        }
        public async Task<bool> AddLog(LogType type, string description, string detail)
        {
            try
            {
                var log = new Log
                {
                    Uuid = UuidV7Helper.NewUuidV7(),
                    LogType = type.ToString(),
                    Description = description,
                    Detail = detail,
                    CreatedAt = DateTime.Now
                };
                await _logRepository.AddLogAsync(log);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建log时出错");
                return false;
            }
        }
        public async Task<bool> AddLog(LogType type, string description, string detail, Guid objectUuid)
        {
            try
            {
                var log = new Log
                {
                    Uuid = UuidV7Helper.NewUuidV7(),
                    LogType = type.ToString(),
                    Description = description,
                    Detail = detail,
                    CreatedAt = DateTime.Now,
                    ObjectUuid = objectUuid
                };
                await _logRepository.AddLogAsync(log);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建log时出错");
                return false;
            }
        }
        public async Task<bool> AddLog(LogType type, string description, string detail, Guid objectUuid, string datajson)
        {
            try
            {
                var log = new Log
                {
                    Uuid = UuidV7Helper.NewUuidV7(),
                    LogType = type.ToString(),
                    Description = description,
                    Detail = detail,
                    CreatedAt = DateTime.Now,
                    DataJson = datajson,
                    ObjectUuid = objectUuid
                };
                await _logRepository.AddLogAsync(log);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建log时出错");
                return false;
            }
        }
        public async Task<List<Log>> GetLogs(LogQueryOptions queryOptions)
        {
            try
            {
                var query = _logRepository.QueryLogs();

                if (queryOptions.Uuid != null)
                {
                    query = query.Where(l => l.Uuid == queryOptions.Uuid);
                }
                if (queryOptions.Type.HasValue)
                {
                    query = query.Where(l => l.LogType == queryOptions.Type.Value.ToString());
                }
                if (queryOptions.Start.HasValue && queryOptions.End.HasValue)
                {
                    query = query.Where(l => l.CreatedAt <= queryOptions.End.Value && l.CreatedAt >= queryOptions.Start);
                }
                if (queryOptions.PageNumber.HasValue && queryOptions.PageSize.HasValue)
                {
                    if (queryOptions.PageSize <= 0) queryOptions.PageSize = 10;
                    if (queryOptions.PageNumber <= 0) queryOptions.PageNumber = 1;

                    query = query
                        .OrderByDescending(l => l.CreatedAt)
                        .Skip((queryOptions.PageNumber ?? 1 - 1) * queryOptions.PageSize ?? 10)
                        .Take(queryOptions.PageSize ?? 10);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询日志时出错");
                return null;
            }
        }
        /*public async Task<List<Log>> GetLog(byte[] uuidBytes)
        {
            try
            {
                var logs = await _logRepository.GetLog(uuidBytes);
                return logs ?? new List<Log>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询日志时出错");
                return null;
            }
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, string type)
        {
            try
            {
                var logs = await _logRepository.GetLog(uuidBytes, type);
                return logs ?? new List<Log>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询日志时出错");
                return null;
            }
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes, string type, DateTime start, DateTime end)
        {
            try
            {
                var logs = await _logRepository.GetLog(uuidBytes, type, start, end);
                return logs ?? new List<Log>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询日志时出错");
                return null;
            }
        }
        public async Task<List<Log>> GetLog(string type, DateTime start, DateTime end)
        {
            try
            {
                var logs = await _logRepository.GetLog(type, start, end);
                return logs ?? new List<Log>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询日志时出错");
                return null;
            }
        }*/

    }
}
