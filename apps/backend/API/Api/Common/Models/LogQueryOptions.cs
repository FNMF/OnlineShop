using API.Domain.Enums;

namespace API.Api.Common.Models
{
    public class LogQueryOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public byte[]? UuidBytes { get; set; }
        public LogType? Type { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
