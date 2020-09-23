using System;
using System.Collections.Generic;
using System.Text;

namespace BuyOrderCalc.Domain
{
    public class SupplyType: Entity
    {
        public string Name { get; set; }
        public int PricePercentModifier { get; set; }
        public double CorpCreditPercent { get; set; }
    }
}
