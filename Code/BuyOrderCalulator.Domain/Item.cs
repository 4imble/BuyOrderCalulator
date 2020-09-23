
namespace BuyOrderCalc.Domain
{
    public class Item: Entity
    {
        public string Name { get; set; }
        public int MarketPrice { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }

        public int TypeId { get; set; }
        public ItemType Type { get; set; }

        public int SupplyTypeId { get; set; }
        public SupplyType SupplyType { get; set; }
    }
}
