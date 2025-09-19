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
                o => !string.IsNullOrEmpty(dto.OrderSid)&&dto.OrderSid.Length<10,
                o => !string.IsNullOrEmpty(dto.OrderMa)&&dto.OrderMa.Length<255,
                o => !string.IsNullOrEmpty(dto.OrderUa)&&dto.OrderUa.Length<255,
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
                dto.Note,
                dto.OrderExpectedTime,
                
                new List<OrderItem>()
                );

            return Result<OrderMain>.Success(orderMain);
        }
        public static Result<Order> ToEntity(OrderMain orderMain)
        {
            var order = new Order
            {
                Uuid = orderMain.OrderUuid,
                UserUuid = orderMain.OrderUseruuid,
                Total = orderMain.OrderTotal,
                OrderStatus = orderMain.OrderStatus.ToString(),
                ShortId = orderMain.OrderSid,
                CreatedAt = orderMain.OrderTime,
                MerchantAddress = orderMain.OrderMa,
                UserAddress = orderMain.OrderUa,
                ListCost = orderMain.OrderCost,
                PackingCost = orderMain.OrderPackingcharge,
                RiderCost = orderMain.OrderRidercost,
                OrderRiderService = orderMain.OrderRiderservice.ToString(),
                Orderitems = orderMain.OrderItems.Select(item => new Orderitem
                {
                    Uuid = item.OrderItemUuid,
                    OrderUuid = orderMain.OrderUuid,
                    ProductUuid = item.ProductUuid,
                    Name = item.Name,
                    Price = item.UnitPrice,
                    Quantity = item.Quantity,
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
                oi.Uuid,
                oi.ProductUuid,
                oi.Quantity,
                oi.Price,
                oi.Name,
                oi.PackingFee
                )).ToList() ?? new List<OrderItem>();
            
            var orderMain = new OrderMain(
                order.Uuid,
                order.UserUuid,
                order.Total,
                Enum.Parse<OrderStatus>(order.OrderStatus),
                order.ShortId ?? string.Empty,
                order.CreatedAt,
                order.MerchantAddress,
                order.UserAddress,
                order.ListCost,
                order.PackingCost,
                order.RiderCost,
                Enum.Parse<OrderRiderService>(order.OrderRiderService),
                order.Note,
                order.ExpectedTime,
                orderItems
            );
            return Result<OrderMain>.Success(orderMain);
        }

    }
}
