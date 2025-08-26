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
        public static Result<OrderMain> CreateOrderMain(OrderMainCreateDto dto)
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
            }
            ;

            var orderMain = new OrderMain(
                UuidV7Helper.NewUuidV7(),
                dto.OrderUseruuid,
                dto.OrderTotal,
                dto.OrderStatus,
                dto.OrderSid,
                DateTime.Now,
                dto.OrderMa,
                dto.OrderUa,
                dto.OrderCost,
                dto.OrderPackingcharge,
                dto.OrderRidercost,
                dto.OrderRiderservice,
                new List<OrderItem>()
                );

            return Result<OrderMain>.Success(orderMain);
        }
        public static Result<Order> ToEntity(OrderMain orderMain)
        {
            var order = new Order
            {
                OrderUuid = orderMain.OrderUuid,
                OrderUseruuid = orderMain.OrderUseruuid,
                OrderTotal = orderMain.OrderTotal,
                OrderStatus = orderMain.OrderStatus,
                OrderSid = orderMain.OrderSid,
                OrderTime = orderMain.OrderTime,
                OrderMa = orderMain.OrderMa,
                OrderUa = orderMain.OrderUa,
                OrderCost = orderMain.OrderCost,
                OrderPackingcharge = orderMain.OrderPackingcharge,
                OrderRidercost = orderMain.OrderRidercost,
                OrderRiderservice = orderMain.OrderRiderservice,
                Orderitems = orderMain.OrderItems.Select(item => new Orderitem
                {
                    OrderitemUuid = item.OrderItemUuid,
                    OrderitemOrderuuid = orderMain.OrderUuid,
                    OrderitemProductuuid = item.ProductUuid,
                    OrderitemName = item.Name,
                    OrderitemPrice = item.UnitPrice,
                    OrderitemQuantity = item.Quantity,
                }).ToList()
            };
            return Result<Order>.Success(order);
        }
        public static Result<OrderMain> ToAggregate(Order order)
        {
            if (order == null)
            {
                return Result<OrderMain>.Fail(ResultCode.NotFound, "订单不存在");
            }
            var orderItems = order.Orderitems?.Select(oi => new OrderItem(
                oi.OrderitemUuid,
                oi.OrderitemProductuuid,
                oi.OrderitemQuantity,
                oi.OrderitemPrice,
                oi.OrderitemName
                )).ToList() ?? new List<OrderItem>();

            var orderMain = new OrderMain(
                order.OrderUuid,
                order.OrderUseruuid,
                order.OrderTotal,
                order.OrderStatus,
                order.OrderSid ?? string.Empty,
                order.OrderTime,
                order.OrderMa,
                order.OrderUa,
                order.OrderCost,
                order.OrderPackingcharge,
                order.OrderRidercost,
                order.OrderRiderservice,
                orderItems
            );
            return Result<OrderMain>.Success(orderMain);
        }

    }
}
