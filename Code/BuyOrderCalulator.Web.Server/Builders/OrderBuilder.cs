using BuyOrderCalc.Domain;
using BuyOrderCalc.Web.Server.Models.ToClient;
using System.Linq;

namespace BuyOrderCalc.Web.Server.Builders
{
    public static class OrderBuilder
    {
        public static OrderModel BuildForView(this Order order)
        {
            return new OrderModel
            {
                Id = order.Id,
                Guid = order.Guid,
                OrderItems = order.OrderItems.Select(x => x.BuildForView()).ToList(),
                DateCreated = order.DateCreated,
                State = order.State
            };
        }

        public static OrderItemModel BuildForView(this OrderItem orderItem)
        {
            return new OrderItemModel
            {
                Id = orderItem.Id,
                Quantity = orderItem.Quantity,
                FixedCorpCreditPercent = orderItem.FixedCorpCreditPercent,
                FixedUnitPrice = orderItem.FixedUnitPrice,
                ItemName = orderItem.Item.Name,
                ItemId = orderItem.Item.Id
            };
        }
    }
}