using BuyOrderCalc.Domain;
using System;
using System.Collections.Generic;

namespace BuyOrderCalc.Web.Server.Models.ToClient
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string DateCreated { get; set; }
        public string DateCredited { get; set; }
        public string DateAccepted { get; set; }
        public bool IsCancelled { get; internal set; }


        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
        public string UserNameDisplay { get; internal set; }
        public string UserAvatarLink { get; internal set; }
    }

    public class OrderItemModel
    {
        public int Quantity { get; set; }
        public double FixedUnitPrice { get; set; }
        public double FixedCorpCreditPercent { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Id { get; internal set; }
    }
}