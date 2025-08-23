using API.Common.Models.Results;

namespace API.Domain.Services.UserPart.Interfaces
{
    public interface IUserRemoveService
    {
        Task<Result> RemoveUserAsync(Guid userUuid);
    }
}
