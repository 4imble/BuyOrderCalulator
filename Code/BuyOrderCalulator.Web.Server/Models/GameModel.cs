using System;
using System.Collections.Generic;

namespace BuyOrderCalc.Domain
{
    public class ItemModel
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderCreditValue { get; set; }
        public bool TakingOrders { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}