namespace BuyOrderCalc.Domain
{
    public class ItemModel
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderCreditValue { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public int SupplyTypeId { get; set; }
        public string SupplyTypeName { get; set; }
        public int SupplyPriceModifier { get; set; }
    }
}