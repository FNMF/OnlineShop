using API.Domain.Enums;

namespace API.Application.OrderCase.DTOs
{
    public class OrderPaymentDto
    {
        public Guid OrderUuid { get; set; }
        public OrderChannel OrderChannel { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
        public string MchId { get; set; }
        public string OutTradeNo { get; set; }
        public string NotifyUrl { get; set; }
        public class AmountInfo 
        {
            public string Total { get; set; }
            public string Currency { get; set; }
        }
        public string OpenId { get; set; }

        public OrderPaymentDto(Guid orderUuid, OrderChannel orderChannel, string description, string appId, string mchId, string outTradeNo, string notifyUrl, string openId)
        {
            OrderUuid = orderUuid;
            OrderChannel = orderChannel;
            Description = description;
            AppId = appId;
            MchId = mchId;
            OutTradeNo = outTradeNo;
            NotifyUrl = notifyUrl;
            OpenId = openId;
        }
    }
}
