using System;
using System.Collections.Generic;
using System.Text;

namespace BuyOrderCalc.Domain
{
    public class SupplyType: Entity
    {
        public string Name { get; set; }
        public int PriceModifier { get; set; }
    }
}
