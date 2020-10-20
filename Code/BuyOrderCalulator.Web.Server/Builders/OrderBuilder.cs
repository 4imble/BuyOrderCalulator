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
                DateCreated = order.DateCreated.ToString("dd, MMM yyyy"),
                DateAccepted = order.DateAccepted.HasValue ? order.DateAccepted.Value.ToString("dd, MMM yyyy") : "",
                DateCredited = order.DateCredited.HasValue ? order.DateCredited.Value.ToString("dd, MMM yyyy") : "",
                IsCancelled = order.IsCancelled,
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