namespace API.Domain.ValueObjects
{
    public record RiderInfo(
        string Name,
        string Phone,
        string? Location = null,
        string? AvatarUrl = null,
        string? Provider = null
        );
}
