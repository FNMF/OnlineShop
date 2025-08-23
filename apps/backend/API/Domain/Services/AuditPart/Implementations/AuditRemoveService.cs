using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.AuditPart.Interfaces;

namespace API.Domain.Services.AuditPart.Implementations
{
    public class AuditRemoveService:IAuditRemoveService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ILogger<AuditRemoveService> _logger;

        public AuditRemoveService(IAuditRepository auditRepository, ILogger<AuditRemoveService> logger)
        {
            _auditRepository = auditRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveAuditAsync(Guid auditUuid)
        {
            try
            {
                if (auditUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _auditRepository.QueryAudits();

                var audit = query.FirstOrDefault(a =>a.AuditUuid == auditUuid && a.AuditIsdeleted == false);

                if (audit == null)
                {
                    return Result.Fail(ResultCode.NotFound, "审核记录不存在或已删除");
                }

                audit.AuditIsdeleted = true;
                await _auditRepository.UpdateAuditAsync(audit);

                return Result.Success();

            }catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
