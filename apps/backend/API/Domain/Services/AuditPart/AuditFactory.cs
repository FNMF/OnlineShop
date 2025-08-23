using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.RegularExpressions;

namespace API.Domain.Services.AuditPart
{
    public class AuditFactory
    {
        public static Result<Audit> Create(AuditCreateDto dto)
        {
            var validations = new List<Func<AuditCreateDto, bool>>
            {
            };

            var validationMessages = new List<string>();
            foreach (var validation in validations)
            {
                if (!validation(dto))
                {
                    validationMessages.Add("数据不合法");
                }
            }

            if (validationMessages.Any())
            {
                return Result<Audit>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            }
            ;
            var audit = new Audit
            {
                AuditObjectuuid = dto.AuditObjectuuid,
                AuditType = dto.AuditType,
                AuditSubmitteruuid = dto.AuditSubmitteruuid,
                AuditSubmitertype = dto.AuditSubmitertype,
                AuditAuditoruuid = dto.AuditAuditoruuid,
                AuditCreatedat = dto.AuditCreatedat,
                AuditReviewedat = dto.AuditReviewedat,
                AuditGroupuuid = dto.AuditGroupuuid,
                AuditUuid = UuidV7Helper.NewUuidV7(),
                AuditStatus = AuditStatus.pending.ToString(),
                AuditReason = "未审核",
                AuditIsdeleted = false,
            };
            return Result<Audit>.Success(audit);
        }
    }
}
