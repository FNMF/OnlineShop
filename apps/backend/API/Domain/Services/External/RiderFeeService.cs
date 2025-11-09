using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.ValueObjects;

namespace API.Domain.Services.External
{
    public class RiderFeeService: IRiderFeeService
    {
        private readonly ILogger<RiderFeeService> _logger;

        public RiderFeeService(ILogger<RiderFeeService> logger)
        {
            _logger = logger;
        }

        public async Task<Result<RiderFeeMain>> GetRiderFeeMainAsync(RiderFeeCreateDto dto) 
        {
            switch (dto) 
            {
                case { RiderService : OrderRiderService.immediate }:
                    //EventBus 返回通知等
                    return Result<RiderFeeMain>.Success(new RiderFeeMain
                    {
                        //第三方接口调用
                        //模拟返回数据
                        //TODO
                        Amount = 10.0m,
                        Description = "平台配送费",
                        CreatedAt = DateTime.UtcNow,
                        OrderId = dto.OrderUuid,
                        Rider = new RiderInfo
                        (
                            "第三方平台骑手",
                            "1234567890"
                        )
                    });
                case { RiderService: OrderRiderService.scheduled}:
                    //EventBus 返回通知等
                    return Result<RiderFeeMain>.Success(new RiderFeeMain
                    {
                        Amount = 5.0m,
                        Description = "商家配送费",
                        CreatedAt = DateTime.UtcNow,
                        OrderId = dto.OrderUuid,
                        Rider = new RiderInfo
                        (
                            "商家骑手",
                            "0987654321"
                        )
                    });
                case { RiderService: OrderRiderService.pickup }:
                    //EventBus 返回通知等
                    return Result<RiderFeeMain>.Success(new RiderFeeMain
                    {
                        Amount = 0.0m,
                        Description = "用户自取，无需配送费",
                        CreatedAt = DateTime.UtcNow,
                        OrderId = dto.OrderUuid,
                        Rider = new RiderInfo
                        (
                            "用户自取",
                            "N/A"
                        )
                    });
                case { RiderService: OrderRiderService.preorder }:
                //EventBus 返回通知等
                    return Result<RiderFeeMain>.Success(new RiderFeeMain
                    {
                        Amount = 0.0m,
                        Description = "预订单，无需配送费",
                        CreatedAt = DateTime.UtcNow,
                        OrderId = dto.OrderUuid,
                        Rider = new RiderInfo
                        (
                            "预订单记录",
                            "N/A"
                        )
                    });

                default:
                    _logger.LogWarning("未知的骑手服务类型");
                    return Result<RiderFeeMain>.Fail(ResultCode.InvalidInput, "未知的骑手服务类型");
            }

        }
    }
}
