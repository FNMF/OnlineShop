using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using Sprache;
using System.Drawing;
using System.Text.RegularExpressions;

namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderFactory
    {
        public Result<OrderMain> CreateOrderMain(OrderMainCreateDto dto)
        {
            var validations = new List<Func<OrderMainCreateDto, bool>>
            {
                o => Enum.TryParse<OrderStatus>(dto.OrderStatus, true, out var result) && Enum.IsDefined(typeof(OrderStatus), result),
                o => !string.IsNullOrEmpty(dto.OrderSid)&&dto.OrderSid.Length<10,
                o => !string.IsNullOrEmpty(dto.OrderMa)&&dto.OrderMa.Length<255,
                o => !string.IsNullOrEmpty(dto.OrderUa)&&dto.OrderUa.Length<255,
                o => Enum.TryParse<OrderRiderService>(dto.OrderStatus, true, out var result) && Enum.IsDefined(typeof(OrderRiderService), result),
                o => dto.OrderTotal>0&&Regex.IsMatch(o.OrderTotal.ToString(), @"^(?:\d{1,6}|\d{1,6}\.\d{1,2})$"),
                o => dto.OrderCost>0&&Regex.IsMatch(o.OrderCost.ToString(), @"^(?:\d{1,6}|\d{1,6}\.\d{1,2})$"),
                o => dto.OrderPackingcharge>0&&Regex.IsMatch(o.OrderPackingcharge.ToString(), @"^(?:\d{1,6}|\d{1,6}\.\d{1,2})$"),
                o => dto.OrderRidercost>0&&Regex.IsMatch(o.OrderRidercost.ToString(), @"^(?:\d{1,6}|\d{1,6}\.\d{1,2})$"),
            };

            var validationMessages = new List<string>();
            foreach (var validation in validations)
            {
                if (!validation(dto))
                {
                    validationMessages.Add("数据不合法");
                }
            }

            if (validationMessages.Any())
            {
                return Result<OrderMain>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var orderMain = new OrderMain
            {
                OrderUuid = UuidV7Helper.NewUuidV7(),
                OrderUseruuid = dto.OrderUseruuid,
                OrderStatus = dto.OrderStatus,
                OrderTotal = dto.OrderTotal,
                OrderCost = dto.OrderCost,
                OrderSid = dto.OrderSid,
                OrderMa = dto.OrderMa,
                OrderUa = dto.OrderUa,
                OrderPackingcharge = dto.OrderPackingcharge,
                OrderRidercost = dto.OrderRidercost,
                OrderRiderservice = dto.OrderRiderservice,
                OrderTime = DateTime.Now,
            };
            
            return Result<OrderMain>.Success(orderMain);
        }


    }
}
