using API.Application.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Repositories;

namespace API.Application.Services
{
    public class AuditGroupService:IAuditGroupService
    {
        private readonly IAuditGroupRepository _auditGroupRepository;
        private readonly ILogger _logger;

        public AuditGroupService(IAuditGroupRepository auditGroupRepository, ILogger logger)
        {
            _auditGroupRepository = auditGroupRepository;
            _logger = logger;
        }
        public async Task<bool> AddAuditGroup(Auditgroup auditgroup)
        {
            try
            {
                await _auditGroupRepository.AddAuditGroupAsync(auditgroup);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加审核组出错");
                return false;
            }
        }
        public IQueryable<Auditgroup> GetAuditGroups()
        {
            try
            {
                return _auditGroupRepository.QueryAuditGroups();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "获取审核组查询体时出错");
                throw;
            }
        }
        public async Task<bool> UpdateAuditGroup(Auditgroup auditgroup)
        {
            try
            {
                await _auditGroupRepository.UpdateAuditGroupAsync(auditgroup);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新审核组时出错");
                return false;
            }
        }
        public async Task<bool> RemoveAuditGroup(Auditgroup auditgroup)
        {
            try
            {
                await _auditGroupRepository.DeleteAuditGroupAsync(auditgroup);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "物理删除审核组时出错");
                return false;
            }
        }
        public async Task SaveChangesAsync()
        {
            try
            {
                await _auditGroupRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "未能成功保存更改");
                throw;
            }
        }
    }
}
