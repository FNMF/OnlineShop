using API.Domain.Enums;

namespace API.Application.Interfaces
{
    public interface ICurrentService
    {
        bool IsAuthenticated { get; }
        byte[]? CurrentUuid { get; }
        string? CurrentName { get; }
        CurrentType CurrentType { get; }

    }
}
