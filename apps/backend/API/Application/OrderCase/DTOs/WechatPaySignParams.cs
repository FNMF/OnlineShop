using System.Text.Json.Serialization;

namespace API.Application.OrderCase.DTOs
{
    public class WechatPaySignParams
    {
        [JsonPropertyName("timeStamp")]
        public string TimeStamp { get; set; }

        [JsonPropertyName("nonceStr")]
        public string NonceStr { get; set; }

        [JsonPropertyName("package")]
        public string Package { get; set; }

        [JsonPropertyName("signType")]
        public string SignType { get; set; } = "RSA";

        [JsonPropertyName("paySign")]
        public string PaySign { get; set; }
    }
}
