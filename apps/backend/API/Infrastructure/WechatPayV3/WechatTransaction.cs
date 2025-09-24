using System.Text.Json.Serialization;

namespace API.Infrastructure.WechatPayV3
{
    public class WechatTransaction
    {
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        [JsonPropertyName("mchid")]
        public string MchId { get; set; }

        [JsonPropertyName("out_trade_no")]
        public string OutTradeNo { get; set; }

        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }

        [JsonPropertyName("trade_type")]
        public string TradeType { get; set; }

        [JsonPropertyName("trade_state")]
        public string TradeState { get; set; }

        [JsonPropertyName("trade_state_desc")]
        public string TradeStateDesc { get; set; }

        [JsonPropertyName("bank_type")]
        public string BankType { get; set; }

        [JsonPropertyName("amount")]
        public TransactionAmount Amount { get; set; }

        [JsonPropertyName("payer")]
        public TransactionPayer Payer { get; set; }

        [JsonPropertyName("success_time")]
        public DateTimeOffset SuccessTime { get; set; }

        [JsonPropertyName("attach")]
        public string Attach { get; set; }

        [JsonPropertyName("promotion_detail")]
        public PromotionDetail[] PromotionDetail { get; set; }

        [JsonPropertyName("scene_info")]
        public SceneInfo SceneInfo { get; set; }
    }

    public class TransactionAmount
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("payer_total")]
        public int PayerTotal { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class TransactionPayer
    {
        [JsonPropertyName("openid")]
        public string OpenId { get; set; }
    }

    public class PromotionDetail
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("coupon_id")]
        public string CouponId { get; set; }

        [JsonPropertyName("goods_detail")]
        public GoodsDetail[] GoodsDetail { get; set; }
    }

    public class GoodsDetail
    {
        [JsonPropertyName("goods_id")]
        public string GoodsId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }

        [JsonPropertyName("goods_remark")]
        public string GoodsRemark { get; set; }

        [JsonPropertyName("discount_amount")]
        public int DiscountAmount { get; set; }
    }

    public class SceneInfo
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
    }
}
