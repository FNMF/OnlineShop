using API.Application.OrderCase.DTOs;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Entities.Models;
using API.Infrastructure.WechatPayV3;
using Microsoft.Extensions.Options;
using SKIT.FlurlHttpClient.Wechat.TenpayV3;
using SKIT.FlurlHttpClient.Wechat.TenpayV3.Models;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using API.Application.Common.EventBus;
using API.Domain.Events.PaymentCase;

public class WeChatPaymentService : IWeChatPaymentGateway
{
    private readonly WechatTenpayClient _client;
    private readonly WeChatPayOptions _options;
    private readonly IEventBus _eventBus;
    private readonly ILogger<WeChatPaymentService> _logger;

    public WeChatPaymentService(
        WechatTenpayClient client,
        IOptions<WeChatPayOptions> options,
        IEventBus eventBus,
        ILogger<WeChatPaymentService> logger)
    {
        _client = client;
        _options = options.Value;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<Result<PrepayResult>> CreatePrepayAsync(OrderMain orderMain, string openId)
    {
        var request = new CreatePayTransactionJsapiRequest()
        {
            AppId = _options.AppId,
            MerchantId = _options.MchId,
            Description = $"订单#{orderMain.OrderUuid}",
            OutTradeNumber = orderMain.OrderUuid.ToString("N"),
            NotifyUrl = _options.NotifyUrl,
            Amount = new CreatePayTransactionJsapiRequest.Types.Amount()
            {
                Total = (int)(orderMain.OrderTotal * 100)
            },
            Payer = new CreatePayTransactionJsapiRequest.Types.Payer()
            {
                OpenId = openId
            }
        };

        var response = await _client.ExecuteCreatePayTransactionJsapiAsync(request);

        if (response.IsSuccessful())
        {
            var jsapiParams = _client.GenerateParametersForJsapiPayRequest(
                appId: _options.AppId,
                prepayId: response.PrepayId
                );

            var signParams = new WechatPaySignParams
            {
                TimeStamp = jsapiParams["timeStamp"],
                NonceStr = jsapiParams["nonceStr"],
                Package = jsapiParams["package"],
                PaySign = jsapiParams["paySign"],
                SignType = jsapiParams["signType"]
            };

            // 添加新建Payment的Event
            await _eventBus.PublishAsync(new PaymentCreatedEvent
                (
                orderMain,
                _options.AppId,
                _options.MchId,
                openId
                ));

            return Result<PrepayResult>.Success(new PrepayResult
            {
                PrepayId = response.PrepayId,
                SignParams = signParams
            });
        }
        else
        {
            return Result<PrepayResult>.Fail(ResultCode.ServerError, "微信支付下单失败");
        }
    }
    //下单操作，用于前端
    public async Task<CreatePayTransactionJsapiResponse> CreateJsapiTransactionAsync(CreatePayTransactionJsapiRequest req)
    {
        // 这里直接调用 SDK 提供的下单接口
        return await _client.ExecuteCreatePayTransactionJsapiAsync(req);
    }

    public async Task<Result<WechatTransaction>> HandlePayCallbackAsync(
        string timestamp,
        string nonce,
        string signature,
        string serial,
        string body)
    {
        // 1. 验签
        if (!VerifySignature(timestamp, nonce, signature, serial, body))
        {
            _logger.LogWarning("微信支付回调验签失败。body={Body}", body);
            return Result<WechatTransaction>.Fail(ResultCode.BusinessError, "验签失败");
        }

        // 2. 解析回调 JSON
        using var doc = JsonDocument.Parse(body);
        if (!doc.RootElement.TryGetProperty("resource", out var resource))
        {
            return Result<WechatTransaction>.Fail(ResultCode.BusinessError, "没有 resource 字段");
        }

        var resourceType = resource.GetProperty("algorithm").GetString(); // 一般为 "AEAD_AES_256_GCM"
        var associatedData = resource.GetProperty("associated_data").GetString();
        var cipherText = resource.GetProperty("ciphertext").GetString();
        var nonceValue = resource.GetProperty("nonce").GetString();

        if (cipherText == null || nonceValue == null || associatedData == null)
        {
            return Result<WechatTransaction>.Fail(ResultCode.BusinessError, "回调 resource 字段缺失");
        }

        // 3. 解密
        string decryptedJson;
        try
        {
            decryptedJson = AesDecrypt(cipherText, nonceValue, associatedData, _options.MchV3Key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "解密微信支付回调 resource 失败");
            return Result<WechatTransaction>.Fail(ResultCode.BusinessError, "解密失败");
        }

        // 4. 反序列化为对象
        var transaction = JsonSerializer.Deserialize<WechatTransaction>(decryptedJson);

        if (transaction == null)
            return Result<WechatTransaction>.Fail(ResultCode.BusinessError, "反序列化 transaction 失败");

        // 5. 返回结果
        return Result<WechatTransaction>.Success(transaction);
    }

    private string AesDecrypt(string cipherText, string nonce, string associatedData, string apiV3Key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(apiV3Key);
        byte[] nonceBytes = Encoding.UTF8.GetBytes(nonce);
        byte[] aadBytes = Encoding.UTF8.GetBytes(associatedData);
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using var aesGcm = new AesGcm(keyBytes);
        byte[] plainBytes = new byte[cipherBytes.Length - 16]; // GCM tag长度16字节
        byte[] tag = new byte[16];

        Array.Copy(cipherBytes, cipherBytes.Length - 16, tag, 0, 16);
        byte[] cipherTextWithoutTag = new byte[cipherBytes.Length - 16];
        Array.Copy(cipherBytes, 0, cipherTextWithoutTag, 0, cipherBytes.Length - 16);

        aesGcm.Decrypt(nonceBytes, cipherTextWithoutTag, tag, plainBytes, aadBytes);

        return Encoding.UTF8.GetString(plainBytes);
    }
    public static bool VerifySignature(string timestamp, string nonce, string body, string signature, string publicKeyPem)
    {
        string message = $"{timestamp}\n{nonce}\n{body}\n";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] signatureBytes = Convert.FromBase64String(signature);

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem.ToCharArray());

        return rsa.VerifyData(messageBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }
}
