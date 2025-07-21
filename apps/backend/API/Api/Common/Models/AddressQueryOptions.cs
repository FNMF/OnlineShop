namespace API.Api.Common.Models
{
    public class AddressQueryOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Isdefault { get; set; }
        public byte[]? UuidBytes { get; set; }
        public byte[]? AddressUuid { get; set; }
    }
}
