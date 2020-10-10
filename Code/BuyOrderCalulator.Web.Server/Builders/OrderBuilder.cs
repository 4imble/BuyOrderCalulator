using BuyOrderCalc.Domain;
using BuyOrderCalc.Web.Server.Models.ToClient;
using System.Linq;

namespace BuyOrderCalc.Web.Server.Builders
{
    public static class OrderBuilder
    {
        public static OrderViewModel BuildForView(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Guid = order.Guid,
                OrderItems = order.OrderItems.Select(x => x.BuildForView()).ToList(),
                DateCreated = order.DateCreated,
                State = order.State,
                UserNameDisplay = $"{order.User.Username}#{order.User.Discriminator}",
                UserAvatarLink = order.User.AvatarLink
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