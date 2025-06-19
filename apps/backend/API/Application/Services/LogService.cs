using API.Api.Models;
using API.Application.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Services
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
        public async Task<bool> AddLog(string type, string description, string detail)
        {
            try
            {
                var log = new Log
                {
                    LogUuid = Guid.NewGuid().ToByteArray(),
                    LogType = type,
                    LogDescription = description,
                    LogDetail = detail,
                    LogTime = DateTime.Now
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
        public async Task<bool> AddLog(string type, string description, string detail, byte[] objectuuidBytes, string datajson)
        {
            try
            {
                var log = new Log
                {
                    LogUuid = Guid.NewGuid().ToByteArray(),
                    LogType = type,
                    LogDescription = description,
                    LogDetail = detail,
                    LogTime = DateTime.Now,
                    LogDatajson = datajson,
                    LogObjectuuid = objectuuidBytes
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

                if (queryOptions.UuidBytes != null)
                {
                    query = query.Where(l => l.LogUuid == queryOptions.UuidBytes);
                }
                if (queryOptions.Type.HasValue)
                {
                    query = query.Where(l => l.LogType == queryOptions.Type.Value.ToString());
                }
                if (queryOptions.Start.HasValue && queryOptions.End.HasValue)
                {
                    query = query.Where(l => l.LogTime <= queryOptions.End.Value && l.LogTime >= queryOptions.Start);
                }
                if (queryOptions.PageNumber.HasValue && queryOptions.PageSize.HasValue)
                {
                    if (queryOptions.PageSize <= 0) queryOptions.PageSize = 10;
                    if (queryOptions.PageNumber <= 0) queryOptions.PageNumber = 1;

                    query = query
                        .OrderByDescending(l => l.LogTime)
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
