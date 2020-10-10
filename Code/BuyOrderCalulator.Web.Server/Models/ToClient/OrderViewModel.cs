using BuyOrderCalc.Domain;
using System;
using System.Collections.Generic;

namespace BuyOrderCalc.Web.Server.Models.ToClient
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public OrderStatus State { get; set; }
        public DateTime DateCreated { get; set; }

        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
        public string UserNameDisplay { get; internal set; }
        public string UserAvatarLink { get; internal set; }
    }

    public class OrderItemModel
    {
        public int Quantity { get; set; }
        public int FixedUnitPrice { get; set; }
        public double FixedCorpCreditPercent { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Id { get; internal set; }
    }
}