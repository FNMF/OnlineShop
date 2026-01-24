using API.Domain.Entities.Models;

namespace API.Application.Common.DTOs
{
    public class MerchantUpdateDto
    {
        public Guid Uuid { get; }
        public string Name { get; }
        public string Province { get; }
        public string City { get; }
        public string District { get; }
        public string Detail { get; }
        public TimeOnly Businessstart { get; }
        public TimeOnly Businessend { get; }
        public Guid Adminuuid { get; }
        public Decimal DeliveryFee { get; }
        public Decimal MinimumOrderAmount { get; }
        public Decimal? FreeDeliveryThreshold { get; }
        public MerchantUpdateDto(Guid uuid, string name, string province, string city, string district, string detail, TimeOnly businessstart, TimeOnly businessend, Guid adminuuid, Decimal deliveryFee, Decimal minOrderAmount, Decimal freeDeliveryThreshold)
        {
            Uuid = uuid;
            Name = name;
            Province = province;
            City = city;
            District = district;
            Detail = detail;
            Businessstart = businessstart;
            Businessend = businessend;
            Adminuuid = adminuuid;
            DeliveryFee = deliveryFee;
            MinimumOrderAmount = minOrderAmount;
            FreeDeliveryThreshold = freeDeliveryThreshold;
        }
    }
}
