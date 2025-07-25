﻿using API.Application.Interfaces;
using API.Common.Interfaces;

namespace API.Application.Services
{
    public class AuditAndAuditGroupService: IAuditAndAuditGroupService
    {
        private readonly IAuditService _auditService;
        private readonly IAuditGroupService _auditGroupService;
        private readonly ILogService _logService;
        private readonly ILogger<AuditAndAuditGroupService> _logger;

        public AuditAndAuditGroupService(IAuditService auditService, IAuditGroupService auditGroupService, ILogService logService, ILogger<AuditAndAuditGroupService> logger)
        {
            _auditService = auditService;
            _auditGroupService = auditGroupService;
            _logService = logService;
            _logger = logger;
        }

    }
}
