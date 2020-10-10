using System;
using System.Collections.Generic;

namespace BuyOrderCalc.Domain
{
    public class Order: Entity
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus State { get; set; }
        public DateTime DateCreated { get; set; }

        public User User { get; set; }
    }

    public enum OrderStatus
    {
        Open,
        Complete,
        Cancelled
    }
}
