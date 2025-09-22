using System.Text.Json.Serialization;

namespace API.Application.OrderCase.DTOs
{
    public class WechatPrepayRequest
    {
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        [JsonPropertyName("mchid")]
        public string MchId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("out_trade_no")]
        public string OutTradeNo { get; set; }

        [JsonPropertyName("notify_url")]
        public string NotifyUrl { get; set; }

        [JsonPropertyName("amount")]
        public WechatAmount Amount { get; set; }

        [JsonPropertyName("payer")]
        public WechatPayer Payer { get; set; }
    }

    public class WechatAmount
    {
        [JsonPropertyName("total")]
        public int Total { get; set; } // 单位：分

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "CNY";
    }

    public class WechatPayer
    {
        [JsonPropertyName("openid")]
        public string OpenId { get; set; }
    }

}
