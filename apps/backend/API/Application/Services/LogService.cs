using API.Entities.Models;
using API.Repositories;

namespace API.Services
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
        public async Task<bool> CreateLog(string type, string description, string detail)
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
                await _logRepository.AddLog(log);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建log时出错");
                return false;
            }
        }
        public async Task<bool> CreateLog(string type, string description, string detail, byte[] objectuuidBytes, string datajson)
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
                await _logRepository.AddLog(log);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建log时出错");
                return false;
            }
        }
        public async Task<List<Log>> GetLog(byte[] uuidBytes)
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
        }

    }
}
