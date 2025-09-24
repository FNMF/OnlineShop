using Microsoft.AspNetCore.Mvc;
using SKIT.FlurlHttpClient.Wechat.TenpayV3;

namespace API.Infrastructure.test
{
    [ApiController]
    [Route("api/test")]
    public class testController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> test1()
        {
            // 1. 构造一个测试用的 Client
            var options = new WechatTenpayClientOptions()
            {
                MerchantId = "1234567890",
                MerchantV3Secret = "dummy_v3key", // 可以随便填一个，不会发请求
                MerchantCertificateSerialNumber = "serial",
                MerchantCertificatePrivateKey = "-----BEGIN PRIVATE KEY-----\nxxx\n-----END PRIVATE KEY-----",
            };
            var client = new WechatTenpayClient(options);
            test.DumpWechatClientSignatures(client);

            return Ok();
        }
    }
}
