using API.Domain.Enums;

namespace API.Common.Interfaces
{
    public interface ICurrentService
    {
        bool IsAuthenticated { get; }
        byte[]? CurrentUuid { get; }
        string? CurrentName { get; }
        CurrentType CurrentType { get; }

    }
}
