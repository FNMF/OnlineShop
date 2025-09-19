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
                ObjectUuid = dto.AuditObjectuuid,
                AuditType = dto.AuditType,
                SubmitterUuid = dto.AuditSubmitteruuid,
                SubmiterType = dto.AuditSubmitertype,
                AuditorUuid = dto.AuditAuditoruuid,
                CreatedAt = dto.AuditCreatedat,
                ReviewedAt = dto.AuditReviewedat,
                GroupUuid = dto.AuditGroupuuid,
                Uuid = UuidV7Helper.NewUuidV7(),
                AuditStatus = AuditStatus.pending.ToString(),
                Reason = "未审核",
                IsDeleted = false,
            };
            return Result<Audit>.Success(audit);
        }
    }
}
