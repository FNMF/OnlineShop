using API.Application.OrderCase.DTOs;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Entities.Models;
using API.Infrastructure.WechatPayV3;
using Microsoft.Extensions.Options;
using SKIT.FlurlHttpClient.Wechat.TenpayV3;
using SKIT.FlurlHttpClient.Wechat.TenpayV3.Models;

public class WeChatPaymentService : IPaymentGateway
{
    private readonly WechatTenpayClient _client;
    private readonly WeChatPayOptions _options;

    public WeChatPaymentService(
        WechatTenpayClient client,
        IOptions<WeChatPayOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    public async Task<Result<PrepayResult>> CreatePrepayAsync(OrderMain orderMain, string openId)
    {
        var request = new CreatePayTransactionJsapiRequest()
        {
            AppId = _options.AppId,
            MerchantId = _options.MchId,
            Description = $"订单#{orderMain.OrderUuid}",
            OutTradeNumber = orderMain.OrderUuid.ToString(),
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
}
