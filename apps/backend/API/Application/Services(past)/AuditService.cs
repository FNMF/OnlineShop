using API.Application.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Application.Services
{
    public class AuditService:IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ILogger<AuditService> _logger;

        public AuditService(IAuditRepository auditRepository, ILogger<AuditService> logger)
        {
            _auditRepository = auditRepository;
            _logger = logger;
        }
        public async Task<bool> AddAudit(Audit audit)
        {
            try
            {
                await _auditRepository.AddAuditAsync(audit);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加审核出错");
                return false;
            }
        }
        public IQueryable<Audit> GetAudits()
        {
            try
            {
                return _auditRepository.QueryAudits();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "获取审核查询体时出错");
                throw;
            }
        }
        public async Task<bool> UpdateAudit(Audit audit)
        {
            try
            {
                await _auditRepository.UpdateAuditAsync(audit);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新审核时出错");
                return false;
            }
        }
        public async Task<bool> RemoveAudit(Audit audit)
        {
            try
            {
                await _auditRepository.DeleteAuditAsync(audit);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "物理删除审核时出错");
                return false;
            }
        }
        public async Task SaveChangesAsync()
        {
            try
            {
                await _auditRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "未能成功保存更改");
                throw;
            }
        }
    }
}
