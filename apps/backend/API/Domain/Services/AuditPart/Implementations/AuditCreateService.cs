using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.AuditPart.Interfaces;

namespace API.Domain.Services.AuditPart.Implementations
{
    public class AuditCreateService:IAuditCreateService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ILogger<AuditCreateService> _logger;

        public AuditCreateService(IAuditRepository auditRepository, ILogger<AuditCreateService> logger)
        {
            _auditRepository = auditRepository;
            _logger = logger;
        }

        public async Task<Result<Audit>> AddAuditAsync(AuditCreateDto dto)
        {
            try
            {
                var result = AuditFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<Audit>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _auditRepository.AddAuditAsync(result.Data);

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Audit>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
