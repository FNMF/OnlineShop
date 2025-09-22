namespace API.Infrastructure.WechatPayV3
{
    public class WeChatPayOptions
    {
        public string AppId { get; set; }
        public string MchId { get; set; }
        public string MchV3Key { get; set; }
        public string MchCertificateSerialNumber { get; set; }
        public string MchPrivateKey { get; set; } // PEM 格式
        public string WechatPayCertificate { get; set; } // 可选，用于验签
        public string NotifyUrl { get; set; }
    }
}
