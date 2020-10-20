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
        private readonly AuthHelper authHelper;

        public OrderController(DataContext dataContext, AuthHelper authHelper)
        {
            this.dataContext = dataContext;
            this.authHelper = authHelper;
        }


        [HttpGet]
        public List<OrderViewModel> Get()
        {
            var orders = dataContext.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Item)
                .Include(x => x.User)
                .OrderBy(x => x.DateCreated);

            return orders.Select(x => x.BuildForView()).ToList();
        }


        [HttpGet("{guid}")]
        public OrderViewModel Get(Guid guid)
        {
            var order = dataContext.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Item)
                .Include(x => x.User)
                .Single(x => x.Guid == guid);

            return order.BuildForView();
        }

        [HttpPost]
        public string Post(OrderModel model)
        {
            var user = authHelper.GetUser(model);

            var newOrder = new Order()
            {
                Guid = Guid.NewGuid(),
                OrderItems = model.SaleItems.Select(CreateOrderItem).ToList(),
                DateCreated = DateTime.UtcNow,
                User = user
            };

            dataContext.Orders.Add(newOrder);

            return newOrder.Guid.ToString();
        }

        [HttpPost]
        [Route("ProcessOrder")]
        public void ProcessOrder(ProcessOrderModel model)
        {
            if (model.State == OrderStatus.Credited)
                authHelper.EnsureAuditor(model);
            else
                authHelper.EnsureAdmin(model);

            var order = dataContext.Orders.Single(x => x.Guid == model.OrderGuid);

            switch (model.State)
            {
                case OrderStatus.Credited:
                    order.DateCredited = order.DateCredited == null 
                        ? DateTime.Now : (DateTime?)null;
                    break;
                case OrderStatus.Accepted:
                    order.DateAccepted = order.DateAccepted == null
                        ? DateTime.Now : (DateTime?)null;
                    break;
                case OrderStatus.Cancelled:
                    order.IsCancelled = !order.IsCancelled;
                    break;
            }

            dataContext.SaveChanges();
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
