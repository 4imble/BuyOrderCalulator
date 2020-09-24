using Microsoft.AspNetCore.Mvc;
using BuyOrderCalc.Domain;
using BuyOrderCalc.EntityFramework;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BuyOrderCalc.Web.Server.Models.FromClient;
using System;
using BuyOrderCalc.Web.Server.Helpers;
using BuyOrderCalc.Web.Server.Builders;
using BuyOrderCalc.Web.Server.Models.ToClient;

namespace BuyOrderCalc.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly DataContext dataContext;

        public OrderController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        [HttpGet]
        public List<OrderModel> Get()
        {
            var orders = dataContext.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Item)
                .Where(x => x.State == OrderStatus.Open);

            return orders.Select(x => x.BuildForView()).ToList();
        }


        [HttpGet("{guid}")]
        public OrderModel Get(Guid guid)
        {
            var order = dataContext.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Item)
                .Single(x => x.Guid == guid);

            return order.BuildForView();
        }

        [HttpPost]
        public string Post(List<SaleItem> saleItems)
        {
            var newOrder = new Order()
            {
                Guid = Guid.NewGuid(),
                OrderItems = saleItems.Select(CreateOrderItem).ToList(),
                State = OrderStatus.Open,
                DateCreated = DateTime.UtcNow
            };

            dataContext.Orders.Add(newOrder);

            return newOrder.Guid.ToString();
        }

        private OrderItem CreateOrderItem(SaleItem saleItem)
        {
            var item = dataContext.Items.Include(x => x.SupplyType).Single(x => x.Id == saleItem.ItemId);
            var itemUnitPrice = Helper.GetPercentage(item.MarketPrice, item.SupplyType.PricePercentModifier);

            return new OrderItem {
                ItemId = saleItem.ItemId,
                Quantity = saleItem.Quantity,
                FixedUnitPrice = itemUnitPrice,
                FixedCorpCreditPercent = item.SupplyType.CorpCreditPercent
            };
        }
    }
}
