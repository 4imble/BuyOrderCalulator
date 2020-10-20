using System;
using System.Collections.Generic;

namespace BuyOrderCalc.Domain
{
    public class Order: Entity
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime DateCreated { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCredited { get; set; }
        public bool IsCancelled { get; set; }

        public User User { get; set; }
    }

    public enum OrderStatus
    {
        Credited,
        Accepted,
        Cancelled
    }
}
