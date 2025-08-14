using API.Domain.Enums;

namespace API.Common.Models
{
    public class CurrentPermission
    {
        private string group { get; }
        private Permissions permission { get; }
    }
}
