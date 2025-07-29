using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Domain.Services.AuditPart.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sprache;

namespace API.Domain.Services.AuditPart.Implementations
{
    public class AuditUpdateService:IAuditUpdateService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ILogger<AuditUpdateService> _logger;

        public AuditUpdateService(IAuditRepository auditRepository, ILogger<AuditUpdateService> logger)
        {
            _auditRepository = auditRepository;
            _logger = logger;
        }

        public async Task<Result<Audit>> MarkAsApprovalAsync(byte[] auditUuid)
        {
            try
            {
                var query = _auditRepository.QueryAudits();
                query = query
                    .Where(a=>a.AuditUuid == auditUuid);
                if (!query.Any())
                {
                    return Result<Audit>.Fail(ResultCode.NotFound,"审核信息不存在");
                }
                var audit = await query.FirstOrDefaultAsync();
                audit.AuditStatus = AuditStatus.approval.ToString();
                await _auditRepository.UpdateAuditAsync(audit);

                return Result<Audit>.Success(audit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Audit>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
        public async Task<Result<Audit>> MarkAsRejectionAsync(byte[] auditUuid, string reason)
        {
            try
            {
                var query = _auditRepository.QueryAudits();
                query = query
                    .Where(a => a.AuditUuid == auditUuid);
                if (!query.Any())
                {
                    return Result<Audit>.Fail(ResultCode.NotFound, "审核信息不存在");
                }
                var audit = await query.FirstOrDefaultAsync();
                audit.AuditStatus = AuditStatus.rejection.ToString();
                audit.AuditReason = reason;
                await _auditRepository.UpdateAuditAsync(audit);

                return Result<Audit>.Success(audit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Audit>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
