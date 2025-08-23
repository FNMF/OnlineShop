using API.Domain.Enums;

namespace API.Common.Interfaces
{
    public interface ICurrentService
    {
        bool IsAuthenticated { get; }
        Guid? CurrentUuid { get; }
        Guid RequiredUuid { get; }
        string? CurrentName { get; }
        CurrentType CurrentType { get; }

    }
}
