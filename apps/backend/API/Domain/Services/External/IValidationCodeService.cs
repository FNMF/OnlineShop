using API.Common.Models.Results;

namespace API.Domain.Services.External
{
    public interface IValidationCodeService
    {
        Task<Result> GenerateValidationCodeAsync(string phone);
    }
}
