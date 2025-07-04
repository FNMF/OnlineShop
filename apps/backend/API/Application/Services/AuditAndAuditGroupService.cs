﻿using API.Application.Interfaces;

namespace API.Application.Services
{
    public class AuditAndAuditGroupService
    {
        private readonly IAuditService _auditService;
        private readonly IAuditGroupService _auditGroupService;
        private readonly ILogService _logService;
        private ILogger _logger;

        public AuditAndAuditGroupService(IAuditService auditService, IAuditGroupService auditGroupService, ILogService logService, ILogger logger)
        {
            _auditService = auditService;
            _auditGroupService = auditGroupService;
            _logService = logService;
            _logger = logger;
        }

    }
}
