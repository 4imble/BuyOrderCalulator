﻿
namespace BuyOrderCalc.Domain
{
    public class OrderItem: Entity
    {
        public int Quantity { get; set; }
        public double FixedUnitPrice { get; set; }
        public double FixedCorpCreditPercent { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
